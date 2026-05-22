import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiptsList } from './receipts-list';

describe('ReceiptsList', () => {
  let component: ReceiptsList;
  let fixture: ComponentFixture<ReceiptsList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptsList],
    }).compileComponents();

    fixture = TestBed.createComponent(ReceiptsList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
