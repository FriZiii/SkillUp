import { Component, inject, input, OnChanges, SimpleChanges } from '@angular/core';
import { greenShades, YearEarnings } from '../../../models/revenues.model';
import { RevenueService } from '../../../services/revenues.service';
import { ChartModule } from 'primeng/chart';
import { CoursesService } from '../../../../course/services/course.service';

@Component({
  selector: 'app-revenue-line-chart',
  standalone: true,
  imports: [ChartModule],
  templateUrl: './revenue-line-chart.component.html',
  styleUrl: './revenue-line-chart.component.css'
})
export class RevenueLineChartComponent implements OnChanges {
  authorId = input.required<string>();
  year = input.required<number>();

  //Services
  revenueService = inject(RevenueService);
  courseService = inject(CoursesService);

  //Variables
  yearEarnings: YearEarnings | null = null;
  data: any;
  courses = this.courseService.courses;
  documentStyle = getComputedStyle(document.documentElement);
  textColor = this.documentStyle.getPropertyValue('--p-text-color');
  textColorSecondary = this.documentStyle.getPropertyValue('--p-text-muted-color');
  surfaceBorder = this.documentStyle.getPropertyValue('--p-content-border-color');
  options = {
    maintainAspectRatio: false,
    aspectRatio: 0.6,
    plugins: {
        legend: {
            labels: {
                color: this.textColor
            }
        }
    },
    scales: {
        x: {
            ticks: {
                color: this.textColorSecondary
            },
            grid: {
                color: this.surfaceBorder,
                drawBorder: false
            }
        },
        y: {
            ticks: {
                color: this.textColorSecondary
            },
            grid: {
                color: this.surfaceBorder,
                drawBorder: false
            }
        }
    }
};

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['year']){
      this.revenueService.getMonthlyEarningsAndSalesPerCourse(this.authorId(), this.year()).subscribe((res) => {
        this.yearEarnings = res;
        setTimeout(() => {
          const labels = res.monthlyEarnings.map(item => {
            const course = this.courses().find(course => course.id === item.itemId);
            return course ? {id: course.id, title: course.title} : {id: '', title: 'unknown'};
        });
        this.data = {
          labels: res.months,
          datasets: res.monthlyEarnings.map((item, index) => ({
              label: labels.find(x => x.id === item.itemId)?.title,
              data: item.data,
              fill: false,
              borderColor: greenShades[index % greenShades.length],
              tension: 0.4
          }))
      };
      console.log(this.data);
    }, 1000); 
    });
    }
  }
}
