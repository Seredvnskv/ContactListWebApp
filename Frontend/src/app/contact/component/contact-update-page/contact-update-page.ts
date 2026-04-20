import {Component, OnInit, signal} from '@angular/core';
import {ContactService} from '../../service/contact-service';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {ContactUpdateDto} from '../../dto/contact-update-dto';
import {FormsModule} from '@angular/forms';
import {CategoryDto} from '../../dto/category-dto';
import {forkJoin} from 'rxjs';

@Component({
  selector: 'app-contact-update-page',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './contact-update-page.html',
  styleUrl: './contact-update-page.css',
})
export class ContactUpdatePage implements OnInit {
  constructor(public contactService: ContactService, private route: ActivatedRoute, private router: Router) {}

  contactId = signal<string | null>(null);
  loading = signal(false);
  submit = signal(false);
  error = signal('');
  success = signal('');

  categories = signal<CategoryDto[]>([]);

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
  };

  selectedCategory(): CategoryDto | null {
    return this.categories().find(c => c.id === this.form.categoryId) ?? null;
  }

  isBusiness(): boolean {
    return this.selectedCategory()?.categoryName === 'Business';
  }

  isOther(): boolean {
    return this.selectedCategory()?.categoryName === 'Other';
  }

  onCategoryChange(): void {
    this.form.subcategoryId = null;
    this.form.customSubcategory = '';
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.contactId.set(id);

    if (!id) {
      this.error.set('Missing contact id in route.');
      return;
    }

    this.loading.set(true);

    forkJoin({
      categories: this.contactService.getCategories(),
      contact: this.contactService.getContactDetails(id)
    }).subscribe({
      next: ({ categories, contact }) => {
        this.categories.set(categories);

        this.form.name = contact.name;
        this.form.surname = contact.surname;
        this.form.email = contact.email;
        this.form.phoneNumber = contact.phoneNumber;
        this.form.dateOfBirth = contact.dateOfBirth;

        const category = categories.find(c => c.categoryName === contact.contactCategory) ?? null;
        this.form.categoryId = category?.id ?? null;

        if (category?.categoryName === 'Business') {
          const sub = category.subcategories.find(s => s.subcategoryName === (contact.contactSubcategory ?? ''));
          this.form.subcategoryId = sub?.id ?? null;
        }

        if (category?.categoryName === 'Other') {
          this.form.customSubcategory = contact.contactSubcategory ?? '';
        }

        this.loading.set(false);
      },
      error: (err) => {
        this.error.set(err?.error?.message ?? 'Failed to load update data.');
        this.loading.set(false);
      }
    });
  }

  onSubmit(): void {
    const id = this.contactId();
    if (!id) {
      this.error.set('Missing contact id.');
      return;
    }

    this.error.set('');
    this.success.set('');
    this.submit.set(true);

    const dto: ContactUpdateDto = {
      name: this.form.name.trim(),
      surname: this.form.surname.trim(),
      email: this.form.email.trim(),
      phoneNumber: this.form.phoneNumber.trim(),
      dateOfBirth: this.form.dateOfBirth,
      categoryId: this.form.categoryId ?? undefined
    };

    if (this.form.password.trim()) {
      dto.password = this.form.password;
    }

    if (this.isBusiness()) {
      dto.subcategoryId = this.form.subcategoryId ?? undefined;
    }

    if (this.isOther()) {
      dto.customSubcategory = this.form.customSubcategory.trim() || undefined;
    }

    this.contactService.updateContact(id, dto).subscribe({
      next: () => {
        this.submit.set(false);
        this.success.set('Contact updated.');
        this.router.navigate(['/contacts', id]);
      },
      error: (err) => {
        this.error.set(err?.error?.message ?? 'Update failed.');
        this.submit.set(false);
      }
    });
  }
}
