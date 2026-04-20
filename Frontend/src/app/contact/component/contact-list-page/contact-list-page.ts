import {Component, signal, OnInit} from '@angular/core';
import {ContactDto} from '../../dto/contact-dto';
import {ContactService} from '../../service/contact-service';
import {RouterLink} from '@angular/router';
import {AuthService} from '../../../auth/service/auth-service';

@Component({
  selector: 'app-contact-list-page',
  imports: [
    RouterLink
  ],
  templateUrl: './contact-list-page.html',
  styleUrl: './contact-list-page.css',
})
export class ContactListPage implements OnInit {
  constructor(private contactService: ContactService, protected authService: AuthService) {}

  contacts = signal<ContactDto[]>([]);
  loading = signal(false);
  error = signal('');

  ngOnInit(): void {
    this.loading.set(true);
    this.error.set('');
    this.getContacts();
  }

  getContacts(): void {
    this.contactService.getContacts().subscribe({
      next: (contacts) => {
        this.contacts.set(contacts);
        this.loading.set(false);
      },
      error: (err) => {
        const msg = err?.error?.message ?? 'Failed to load contacts.';
        this.error.set(msg);
        this.loading.set(false);
      }
    });
  }

  onDelete(contact: ContactDto) {
    this.contactService.deleteContact(contact.id).subscribe({
      next: () => {
        this.getContacts();
      },
      error: (err) => {
        const msg = err?.error?.message ?? 'Failed to delete contact.';
        this.error.set(msg);
      }
    })
  }
}
