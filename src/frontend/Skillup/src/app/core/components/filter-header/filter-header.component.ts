import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { MegaMenuModule } from 'primeng/megamenu';
import { CategoryService } from '../../../course/services/category.service';
import { Category } from '../../../course/models/category.model';
import { MegaMenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { NgIf } from '@angular/common';
import { SkeletonModule } from 'primeng/skeleton';

@Component({
  selector: 'app-filter-header',
  standalone: true,
  imports: [MenubarModule, NgIf, SkeletonModule],
  templateUrl: './filter-header.component.html',
  styleUrl: './filter-header.component.css',
})
export class FilterHeaderComponent {
  //Services
  categoryService = inject(CategoryService);

  //Variables
  categories = this.categoryService.categories;
  skeletonItems = new Array(11).fill({ label: '1' });

  items = computed(() =>
    this.categories().map((category) => ({
      label: category.name,
      items: [
        {
          label: 'All',
          route: 'courses/' + category.slug + '/all',
        },
        ...category.subcategories.map((subcategory) => ({
          label: subcategory.name,
          route:
            'courses/' +
            category.slug +
            '/' +
            subcategory.slug,
        })),
      ],
    }))
  );

  hideSubMenu() {
    window.document.body.click();
  }
}
