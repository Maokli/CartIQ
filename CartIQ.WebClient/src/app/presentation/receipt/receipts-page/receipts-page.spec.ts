import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { of } from 'rxjs';
import { ReceiptUseCase } from '@domain/useCases/receipt.usecase';

import { ReceiptsPage } from './receipts-page';

describe('ReceiptsPage', () => {
  let component: ReceiptsPage;
  let fixture: ComponentFixture<ReceiptsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptsPage],
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

    fixture = TestBed.createComponent(ReceiptsPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
