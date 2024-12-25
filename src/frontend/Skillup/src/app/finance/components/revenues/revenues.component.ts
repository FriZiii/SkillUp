import { Component, inject, input, OnInit } from '@angular/core';
import { RevenueService } from '../../services/revenues.service';
import { greenShades, ItemEarnings, Revenue, YearEarnings } from '../../models/revenues.model';
import { CoursesService } from '../../../course/services/course.service';
import { ChartModule } from 'primeng/chart';

@Component({
  selector: 'app-revenues',
  standalone: true,
  imports: [ChartModule],
  templateUrl: './revenues.component.html',
  styleUrl: './revenues.component.css'
})
export class RevenuesComponent implements OnInit {
  authorId = input.required<string>();
  
  //Services
  revenueService = inject(RevenueService)
  courseService = inject(CoursesService);

  //Variables
  itemsEarings: ItemEarnings[] = [];
  revenue: Revenue | null = null;

  courses = this.courseService.courses;

  //Charts Data
  courseQuantity: any;
  courseTotals: any;

  ngOnInit(): void {
    this.revenueService.getEarningsAndSalesPerCourse(this.authorId()).subscribe((res) => {
      this.itemsEarings = res;
      setTimeout(() => {
        const labels = res.map(item => {
          const course = this.courses().find(course => course.id === item.itemId);
          return course ? course.title : 'Nieznany';
      });
      
      const dataCounts = res.map(item => item.itemsCount);
      const dataTotals = res.map(item => item.total);

      this.courseQuantity = {
        labels: labels,
        datasets: [
            {
                data: dataCounts,
                backgroundColor: greenShades.slice(0, res.length),
                hoverBackgroundColor: greenShades.slice(1, res.length + 1)
            }
        ]
    };
      this.courseTotals = {
        labels: labels,
        datasets: [
            {
                data: dataTotals,
                backgroundColor: greenShades.slice(0, res.length),
                hoverBackgroundColor: greenShades.slice(1, res.length + 1)
            }
        ]
    };
  }, 2000); 
  });
    this.revenueService.getMonthlyEarningsAndSalesPerCourse(this.authorId(), 2024).subscribe();
    this.revenueService.getRevenue(this.authorId()).subscribe((res) => this.revenue = res);
  }


}


