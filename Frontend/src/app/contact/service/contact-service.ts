import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ContactDto} from '../dto/contact-dto';
import {ContactDetailsDto} from '../dto/contact-details-dto';
import {ContactCreateDto} from '../dto/contact-create-dto';
import {ContactUpdateDto} from '../dto/contact-update-dto';
import {CategoryDto} from '../dto/category-dto';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  constructor(private http: HttpClient) {}

  getContacts(): Observable<ContactDto[]> {
    return this.http.get<ContactDto[]>('/api/contacts');
  }

  getContactDetails(id: string): Observable<ContactDetailsDto> {
    return this.http.get<ContactDetailsDto>(`/api/contacts/${id}`, {
      withCredentials: true
    });
  }

  getCategories(): Observable<CategoryDto[]> {
    return this.http.get<CategoryDto[]>('/api/category');
  }

  createContact(dto: ContactCreateDto): Observable<ContactDto> {
    return this.http.post<ContactDto>('/api/contacts', dto, {
      withCredentials: true
    });
  }

  updateContact(id: string, dto: ContactUpdateDto): Observable<void> {
    return this.http.patch<void>(`/api/contacts/${id}`, dto, {
      withCredentials: true
    });
  }

  deleteContact(id: string): Observable<void> {
    return this.http.delete<void>(`/api/contacts/${id}`, {
      withCredentials: true
    });
  }
}
