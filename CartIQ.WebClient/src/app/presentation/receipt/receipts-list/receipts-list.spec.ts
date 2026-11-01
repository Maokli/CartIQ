import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { of } from 'rxjs';
import { ReceiptUseCase } from '@domain/useCases/receipt.usecase';

import { ReceiptsList } from './receipts-list';

describe('ReceiptsList', () => {
  let component: ReceiptsList;
  let fixture: ComponentFixture<ReceiptsList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptsList],
      providers: [
        provideRouter([]),
        {
          provide: ReceiptUseCase,
          useValue: {
            getReceipts: () => of({ pageIndex: 0, totalPages: 0, pageSize: 10, itemsCount: 0, items: [] }),
          },
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ReceiptsList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
