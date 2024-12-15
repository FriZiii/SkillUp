import { Component, computed, inject, input, OnChanges, OnInit, signal, SimpleChanges } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { UserProgressService } from '../../services/user-progress-service';
import { UserService } from '../../../user/services/user.service';
import { CourseContentService } from '../../services/course-content.service';
import { AccordionModule } from 'primeng/accordion';
import { SectionItemComponent } from "../edit-course/course-creator/section-item/section-item.component";
import { ElementItemDisplayComponent } from '../displays/element-item-display/element-item-display.component';
import { AssetType, Element } from '../../models/course-content.model';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { AssetService } from '../../services/asset.service';
import { VjsPlayerComponent } from '../../../videojs/videojs.component';

@Component({
  selector: 'app-course-walk-through',
  standalone: true,
  imports: [AccordionModule, SectionItemComponent, ElementItemDisplayComponent, PdfViewerModule, VjsPlayerComponent],
  templateUrl: './course-walk-through.component.html',
  styleUrl: './course-walk-through.component.css'
})
export class CourseWalkThroughComponent implements OnInit{
  //URL
  courseId = input.required<string>();

  //Services
  coureContentService = inject(CourseContentService);
  userProgressService = inject(UserProgressService);
  assetService = inject(AssetService);

  //Variables
  AssetType = AssetType;
  sections = computed(() => this.coureContentService.sections());
  private _currentElement: Element | null = null
    fileLink = signal('');

  ngOnInit(): void {
    this.coureContentService.getSectionsByCourseId(this.courseId());

    this.userProgressService.getAcomplishedElementsForCourse(this.courseId()).subscribe(
      (res) => {
        this.userProgressService.accomplishedElements.set(res);
    })

    new Promise(resolve => setTimeout(resolve, 2000))
    .then(() => {
        this.currentElement = this.sections()[0].elements[0];
    });
  
  }

  get currentElement(): Element | null {
    return this._currentElement;
  }

  set currentElement(value: Element) {
    this._currentElement = value;
    if(value.hasAsset){
      this.assetService.getAsset(value.id, value.type).subscribe((response) => {
        this.fileLink.set(response.url);
        console.log(this.fileLink());
        });
    }
}

  onElementClicked(element: Element){
    this.currentElement = element;
  }
}
