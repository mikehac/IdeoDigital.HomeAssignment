import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewInvoiceDialogeComponent } from './new-invoice-dialoge.component';

describe('NewInvoiceDialogeComponent', () => {
  let component: NewInvoiceDialogeComponent;
  let fixture: ComponentFixture<NewInvoiceDialogeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewInvoiceDialogeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewInvoiceDialogeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
