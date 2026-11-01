import { Component } from '@angular/core';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { lucidePlus } from '@ng-icons/lucide';
import { HlmButtonImports } from '@spartan-ng/helm/button';
import { HlmCardImports } from '@spartan-ng/helm/card';
import { HlmSeparatorImports } from '@spartan-ng/helm/separator';
import { HlmTypographyImports } from '@spartan-ng/helm/typography';
import { ReceiptsList } from '../receipts-list/receipts-list';

@Component({
  selector: 'app-receipts-page',
  imports: [
    NgIcon,
    ...HlmButtonImports,
    ...HlmCardImports,
    ...HlmSeparatorImports,
    ...HlmTypographyImports,
    ReceiptsList,
  ],
  providers: [provideIcons({ lucidePlus })],
  templateUrl: './receipts-page.html',
})
export class ReceiptsPage {
  openAddReceiptModal() {}
}
