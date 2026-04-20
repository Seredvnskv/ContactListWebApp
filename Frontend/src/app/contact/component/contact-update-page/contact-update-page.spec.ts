import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactUpdatePage } from './contact-update-page';

describe('ContactUpdatePage', () => {
  let component: ContactUpdatePage;
  let fixture: ComponentFixture<ContactUpdatePage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactUpdatePage],
    }).compileComponents();

    fixture = TestBed.createComponent(ContactUpdatePage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
