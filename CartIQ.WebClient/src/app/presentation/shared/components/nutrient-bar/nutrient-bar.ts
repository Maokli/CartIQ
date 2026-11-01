import { Nutrients } from '@domain/models/shared/nutrients';
import { DecimalPipe } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import { BrnTooltip, provideBrnTooltipDefaultOptions } from '@spartan-ng/brain/tooltip';
import { DEFAULT_TOOLTIP_CONTENT_CLASSES } from '@spartan-ng/helm/tooltip';
import { HlmTypographyImports } from '@spartan-ng/helm/typography';
import { hlm } from '@spartan-ng/helm/utils';

interface MacroSegment {
  label: string;
  value: number;
  colorClass: string;
}

const MACROS: { key: keyof Nutrients; label: string; colorClass: string }[] = [
  { key: 'protein', label: 'Protein', colorClass: 'bg-nutrient-protein' },
  { key: 'carbs', label: 'Carbs', colorClass: 'bg-nutrient-carbs' },
  { key: 'fat', label: 'Fat', colorClass: 'bg-nutrient-fat' },
];

@Component({
  selector: 'app-nutrient-bar',
  imports: [DecimalPipe, BrnTooltip, ...HlmTypographyImports],
  providers: [
    // The stock tooltip surface is a fixed bg-foreground panel; strip it so each
    // segment's template can paint the surface in its own nutrient color.
    provideBrnTooltipDefaultOptions({
      showDelay: 150,
      tooltipContentClasses: hlm(DEFAULT_TOOLTIP_CONTENT_CLASSES, 'bg-transparent p-0'),
      svgClasses: 'hidden',
    }),
  ],
  host: { class: 'flex items-center gap-2.5' },
  templateUrl: './nutrient-bar.html',
})
export class NutrientBar {
  nutrients = input.required<Nutrients>();

  segments = computed<MacroSegment[]>(() =>
    MACROS.map(({ key, label, colorClass }) => ({
      label,
      value: this.nutrients()[key],
      colorClass,
    })).filter((segment) => segment.value > 0),
  );

  macroTotal = computed(() => this.segments().reduce((sum, segment) => sum + segment.value, 0));

  ariaLabel = computed(() =>
    this.segments()
      .map((segment) => `${segment.label} ${Math.round(segment.value)} g`)
      .join(', '),
  );
}
