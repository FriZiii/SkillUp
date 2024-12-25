import { Component, inject, input, OnChanges, SimpleChanges } from '@angular/core';
import { YearEarnings } from '../../../models/revenues.model';
import { RevenueService } from '../../../services/revenues.service';

@Component({
  selector: 'app-revenue-line-chart',
  standalone: true,
  imports: [],
  templateUrl: './revenue-line-chart.component.html',
  styleUrl: './revenue-line-chart.component.css'
})
export class RevenueLineChartComponent implements OnChanges {
  authorId = input.required<string>();
  year = input.required<number>();

  //Services
  revenueService = inject(RevenueService);

  //Variables
  yearEarnings: YearEarnings | null = null;

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['year']){
      this.revenueService.getMonthlyEarningsAndSalesPerCourse(this.authorId(), this.year()).subscribe((res) => this.yearEarnings = res);
    }
  }
}
