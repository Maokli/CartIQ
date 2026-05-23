import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiptsPage } from './receipts-page';

describe('ReceiptsPage', () => {
  let component: ReceiptsPage;
  let fixture: ComponentFixture<ReceiptsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptsPage],
    }).compileComponents();

    fixture = TestBed.createComponent(ReceiptsPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
