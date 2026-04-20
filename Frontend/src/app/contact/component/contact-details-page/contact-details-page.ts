import { Component, OnInit, signal } from '@angular/core';
import {ContactService} from '../../service/contact-service';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {ContactDetailsDto} from '../../dto/contact-details-dto';

@Component({
  selector: 'app-contact-details-page',
  imports: [
    RouterLink
  ],
  templateUrl: './contact-details-page.html',
  styleUrl: './contact-details-page.css',
})
export class ContactDetailsPage implements OnInit {
  constructor(private contactService: ContactService, private route: ActivatedRoute) {}

  contact = signal<ContactDetailsDto | null>(null);
  loading = signal(false);
  error = signal('');

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      this.error.set('Missing contact id in route.');
      return;
    }

    this.loading.set(true);
    this.error.set('');

    this.contactService.getContactDetails(id).subscribe({
      next: (details) => {
        this.contact.set(details);
        this.loading.set(false);
      },
      error: (err) => {
        const msg = err?.error?.message ?? 'Failed to load contact details.';
        this.error.set(msg);
        this.loading.set(false);
      }
    });
  }
}
