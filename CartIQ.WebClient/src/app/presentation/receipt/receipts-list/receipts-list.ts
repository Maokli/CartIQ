import { DEFAULT_PAGE_SIZE } from '@domain/base/constants';
import { PaginationResult } from '@domain/base/paginationResult';
import { Receipt } from '@domain/models/receipt/receipt';
import { ReceiptUseCase } from '@domain/useCases/receipt.usecase';
import { NutrientBar } from '@presentation/shared/components/nutrient-bar/nutrient-bar';
import { DatePipe, DecimalPipe, SlicePipe, UpperCasePipe } from '@angular/common';
import { Component, effect, signal } from '@angular/core';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { lucideChevronRight, lucideReceipt } from '@ng-icons/lucide';
import { HlmAvatarImports } from '@spartan-ng/helm/avatar';
import { HlmBadgeImports } from '@spartan-ng/helm/badge';
import { HlmButtonImports } from '@spartan-ng/helm/button';
import { HlmEmptyImports } from '@spartan-ng/helm/empty';
import { HlmItemImports } from '@spartan-ng/helm/item';
import { HlmNumberedPagination } from '@spartan-ng/helm/pagination';
import { HlmSeparatorImports } from '@spartan-ng/helm/separator';
import { HlmSpinnerImports } from '@spartan-ng/helm/spinner';
import { HlmTableImports } from '@spartan-ng/helm/table';
import { HlmTypographyImports } from '@spartan-ng/helm/typography';
import { catchError, finalize, of } from 'rxjs';

const CATEGORY_CLASSES: Record<string, string> = {
  groceries: 'bg-status-success text-status-success-foreground',
  health: 'bg-status-warning text-status-warning-foreground',
  mixed: 'bg-status-info text-status-info-foreground',
};

@Component({
  selector: 'app-receipts-list',
  imports: [
    DatePipe,
    DecimalPipe,
    SlicePipe,
    UpperCasePipe,
    NgIcon,
    ...HlmTableImports,
    ...HlmBadgeImports,
    ...HlmButtonImports,
    ...HlmSpinnerImports,
    ...HlmEmptyImports,
    ...HlmAvatarImports,
    ...HlmItemImports,
    ...HlmSeparatorImports,
    ...HlmTypographyImports,
    HlmNumberedPagination,
    NutrientBar,
  ],
  providers: [provideIcons({ lucideChevronRight, lucideReceipt })],
  templateUrl: './receipts-list.html',
})
export class ReceiptsList {
  displayedColumns = ['merchant', 'category', 'items', 'nutrients', 'totalAmount', 'actions'];

  receipts = signal<Receipt[]>([]);
  itemsCount = signal(0);
  loading = signal(false);

  currentPage = signal(1);
  itemsPerPage = signal(DEFAULT_PAGE_SIZE);

  constructor(private receiptUseCase: ReceiptUseCase) {
    effect(() => {
      const pageIndex = this.currentPage() - 1;
      const pageSize = this.itemsPerPage();
      this.loadReceipts(pageIndex, pageSize);
    });
  }

  categoryClass(category: string): string {
    return CATEGORY_CLASSES[category.toLowerCase()] ?? 'bg-secondary text-secondary-foreground';
  }

  private loadReceipts(pageIndex: number, pageSize: number) {
    this.loading.set(true);

    this.receiptUseCase
      .getReceipts(pageIndex, pageSize)
      .pipe(
        catchError(() => of({} as PaginationResult<Receipt>)),
        finalize(() => this.loading.set(false)),
      )
      .subscribe((res) => {
        this.receipts.set(res.items ?? []);
        this.itemsCount.set(res.itemsCount ?? 0);
      });
  }
}
