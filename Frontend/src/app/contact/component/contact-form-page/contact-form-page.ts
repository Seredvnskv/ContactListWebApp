import {Component, OnInit, signal} from '@angular/core';
import {ContactService} from '../../service/contact-service';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {CategoryDto} from '../../dto/category-dto';
import {ContactCreateDto} from '../../dto/contact-create-dto';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-contact-form-page',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './contact-form-page.html',
  styleUrl: './contact-form-page.css',
})
export class ContactFormPage implements OnInit {
  constructor(private contactService: ContactService, private router: Router) {}

  categories = signal<CategoryDto[]>([]);
  loadingCategories = signal(false);
  submit = signal(false);
  error = signal('');
  success = signal('');

  form = {
    name: '',
    surname: '',
    email: '',
    password: '',
    phoneNumber: '',
    dateOfBirth: '',
    categoryId: null as number | null,
    subcategoryId: null as number | null,
    customSubcategory: ''
  }

  selectedCategory(): CategoryDto | null {
    return this.categories().find(c => c.id === this.form.categoryId) ?? null;
  }

  isBusiness(): boolean {
    return this.selectedCategory()?.categoryName === 'Business';
  }

  isOther(): boolean {
    return this.selectedCategory()?.categoryName === 'Other';
  }

  ngOnInit(): void {
    this.loadingCategories.set(true);

    this.contactService.getCategories().subscribe({
      next: (cats) => {
        this.categories.set(cats);
        this.loadingCategories.set(false);
      },
      error: (err) => {
        this.error.set(err?.error?.message ?? 'Failed to load categories.');
        this.loadingCategories.set(false);
      }
    });
  }

  onCategoryChange(): void {
    this.form.subcategoryId = null;
    this.form.customSubcategory = '';
  }

  private isPasswordStrong(password: string): boolean {
    return /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/.test(password);
  }

  private isEmailValid(email: string): boolean {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
  }

  private isPhoneNumberValid(phoneNumber: string): boolean {
    return /^\+?48\d{9}$/.test(phoneNumber);
  }

  onSubmit(): void {
    this.error.set('');
    this.success.set('');

    if (!this.form.name || !this.form.surname || !this.form.email || !this.form.password ||
      !this.form.phoneNumber || !this.form.dateOfBirth || this.form.categoryId === null)
    {
      this.error.set('All fields are required.');
      return;
    }

    const email = this.form.email.trim();
    const phoneNumber = this.form.phoneNumber.trim();

    if (!this.isEmailValid(email)) {
      this.error.set('Invalid email address format.');
      return;
    }

    if (phoneNumber.length > 20 || !this.isPhoneNumberValid(phoneNumber)) {
      this.error.set('Invalid phone number format. Use +48XXXXXXXXX or 48XXXXXXXXX.');
      return;
    }

    if (!this.isPasswordStrong(this.form.password)) {
      this.error.set('Password must be at least 8 characters long and include uppercase, lowercase letters and a number.');
      return;
    }

    if (this.isBusiness() && this.form.subcategoryId === null) {
      this.error.set('Dla Business wybierz podkategorie.');
      return;
    }

    if (this.isOther() && !this.form.customSubcategory.trim()) {
      this.error.set('Dla Other wpisz podkategorie.');
      return;
    }

    const dto: ContactCreateDto = {
      name: this.form.name.trim(),
      surname: this.form.surname.trim(),
      email,
      password: this.form.password,
      phoneNumber,
      dateOfBirth: this.form.dateOfBirth,
      categoryId: this.form.categoryId
    };

    if (this.isBusiness() && this.form.subcategoryId !== null) {
      dto.subcategoryId = this.form.subcategoryId;
    }

    if (this.isOther()) {
      dto.customSubcategory = this.form.customSubcategory.trim();
    }

    this.submit.set(true);

    this.contactService.createContact(dto).subscribe({
      next: () => {
        this.submit.set(false);
        this.success.set('Contact created successfully.');
        this.router.navigate(['/contacts']);
      },
      error: (err) => {
        this.error.set(err?.error?.message ?? 'Creation failed.');
        this.submit.set(false);
      }
    });
  }
}
