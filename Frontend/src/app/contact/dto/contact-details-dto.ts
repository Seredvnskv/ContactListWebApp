export interface ContactDetailsDto {
  name: string;
  surname: string;
  email: string;
  password: string;
  phoneNumber: string;
  contactCategory: string;
  contactSubcategory?: string | null;
  dateOfBirth: string;
}
