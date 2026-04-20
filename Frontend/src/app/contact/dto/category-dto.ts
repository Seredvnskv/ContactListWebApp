export interface CategorySubcategoryDto {
  id: number;
  subcategoryName: string;
}

export interface CategoryDto {
  id: number;
  categoryName: string;
  subcategories: CategorySubcategoryDto[];
}
