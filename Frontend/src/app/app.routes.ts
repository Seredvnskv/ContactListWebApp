import { Routes } from '@angular/router';
import {ContactListPage} from './contact/component/contact-list-page/contact-list-page';
import {ContactDetailsPage} from './contact/component/contact-details-page/contact-details-page';
import {LoginPage} from './auth/component/login-page/login-page';
import {ContactFormPage} from './contact/component/contact-form-page/contact-form-page';
import {ContactUpdatePage} from './contact/component/contact-update-page/contact-update-page';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/contacts',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginPage
  },
  {
    path: 'contacts',
    component: ContactListPage
  },
  {
    path: 'contacts/new',
    component: ContactFormPage
  },
  {
    path: 'contacts/:id/edit',
    component: ContactUpdatePage
  },
  {
    path: 'contacts/:id',
    component: ContactDetailsPage
  },
  {
    path: '**',
    redirectTo: '/contacts',
  }
];
