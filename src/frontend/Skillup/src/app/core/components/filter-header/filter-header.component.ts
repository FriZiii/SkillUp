import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { MegaMenuModule } from 'primeng/megamenu';
import { CategoryService } from '../../../course/services/category.service';
import { Category } from '../../../course/models/category.model';
import { MegaMenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { NgIf } from '@angular/common';
import { routes } from '../../../app.routes';

@Component({
  selector: 'app-filter-header',
  standalone: true,
  imports: [MenubarModule, NgIf],
  templateUrl: './filter-header.component.html',
  styleUrl: './filter-header.component.css',
})
export class FilterHeaderComponent implements OnInit {
  //Services
  categoryService = inject(CategoryService);

  //Variables
  categories = signal<Category[]>([]);
  items = computed(() =>
    this.categories().map((category) => ({
      label: category.name,
      items: [
        {
          label: 'All',
          route: 'courses/' + category.name.toLowerCase() + '/all',
        },
        ...category.subcategories.map((subcategory) => ({
          label: subcategory.name,
          route:
            'courses/' +
            category.name.toLowerCase() +
            '/' +
            subcategory.name.toLowerCase(),
        })),
      ],
    }))
  );

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((data) => {
      this.categories.set(data);
    });
  }

  hideSubMenu() {
    window.document.body.click();
  }
}
