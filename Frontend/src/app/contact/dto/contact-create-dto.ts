export interface ContactCreateDto {
  name: string;
  surname: string;
  email: string;
  password: string;
  phoneNumber: string;
  dateOfBirth: string;
  categoryId: number;
  subcategoryId?: number;
  customSubcategory?: string;
}
