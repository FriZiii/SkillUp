import { Component, computed, inject, input, OnInit } from '@angular/core';
import { RevenueService } from '../../services/revenues.service';
import { greenShades, ItemEarnings, Revenue, YearEarnings } from '../../models/revenues.model';
import { CoursesService } from '../../../course/services/course.service';
import { ChartModule } from 'primeng/chart';
import { RevenueLineChartComponent } from "./revenue-line-chart/revenue-line-chart.component";
import { SelectModule } from 'primeng/select';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SkeletonModule } from 'primeng/skeleton';

@Component({
  selector: 'app-revenues',
  standalone: true,
  imports: [ChartModule, RevenueLineChartComponent, SelectModule, FormsModule, CommonModule, SkeletonModule],
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
  loading = true;

  courses = this.courseService.courses;

  //Charts Data
  courseQuantity: any;
  courseTotals: any;

  //Select
  years = [
    { name: '2024', value: 2024 },
    { name: '2023', value: 2023 },
    { name: '2022', value: 2022 },
    { name: '2021', value: 2021 },
];
selectedYear = 2024;
authorCourses = computed(() => this.courseService.getCoursesByAuthor(this.authorId()).map((course) => ({
      name: course.title,
      value: course.id
  })));
  selectedCourse: string | null = null;
options = {
  plugins: {
      legend: {
          display: false  // Ukrywa legendę nad wykresem
      },
      tooltip: {
          enabled: true  // Pokazuje etykiety po najechaniu
      }
  }
}

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
    this.loading = false;
  }, 2000); 
  });
    this.revenueService.getRevenue(this.authorId()).subscribe((res) => this.revenue = res);
  }


}


