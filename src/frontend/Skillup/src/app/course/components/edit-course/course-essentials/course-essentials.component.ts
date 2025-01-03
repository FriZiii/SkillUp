import { Component, computed, inject, input, OnInit, signal, WritableSignal } from '@angular/core';
import { FormsModule} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { InputTextModule } from 'primeng/inputtext';
import { CoursesService } from '../../../services/course.service';
import { CourseDetail } from '../../../models/course.model';
import { SelectModule } from 'primeng/select';
import { CourseLevel } from '../../../models/course-level.model';
import { CategoryService } from '../../../services/category.service';
import { FloatLabelModule } from "primeng/floatlabel"
import { PropertiesListComponent } from "./properties-list/properties-list.component";
import { ImageCroperComponent } from "../../../../utils/components/image-croper/image-croper.component";
import { SafeUrl } from '@angular/platform-browser';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputNumberModule } from 'primeng/inputnumber';
import { FinanceService } from '../../../../finance/services/finance.service';
import { ToastHandlerService } from '../../../../core/services/toast-handler.service';

@Component({
  selector: 'app-course-essentials',
  standalone: true,
  imports: [InputTextModule, ButtonModule, FileUploadModule, SelectModule, FormsModule, FloatLabelModule, PropertiesListComponent, ImageCroperComponent, InputGroupModule, InputGroupAddonModule, InputNumberModule],
  templateUrl: './course-essentials.component.html',
  styleUrl: './course-essentials.component.css'
})
export class CourseEssentialsComponent implements OnInit {
  //Services
  courseService = inject(CoursesService)
  courseCategoryService = inject(CategoryService);
  financeService = inject(FinanceService);
  toastService = inject(ToastHandlerService);
  
  //Variables
  courseId = input.required<string>();
  course = signal<CourseDetail | null>(null);

  //Selects
  levels = Object.entries(CourseLevel).map(([name, value]) => ({
    name,
    value
}));

categories = this.courseCategoryService.categories;
  categoryNames = computed(() =>
    this.categories().map((category) => ({
      id: category.id,
      name: category.name,
    }))
  );
  subcategoryNames = computed(() => {
    const selectedCat = this.categories().find(
      (category) => category.id === this.selectedCategory()
    );
    return selectedCat
      ? selectedCat.subcategories.map((sub) => ({ id: sub.id, name: sub.name }))
      : [];
  });

  title = signal('');
  subtitle = signal('');
  description = signal('');
  selectedCategory = signal('');
  selectedSubcategory = signal('');
  selectedLevel = signal<CourseLevel | null>(null);
  price = signal(0);
  
  objectivesList = signal(['']);
  mustKnowBefore = signal(['']);
  intendedFor = signal(['']);


  ngOnInit(): void {
    this.courseService.getCourseDetailById(this.courseId()).subscribe({
      next: (res) => {
        this.course.set(res);
        this.title.set(res.title);
        this.subtitle.set(res.subtitle);
        this.description.set(res.description);
        this.selectedCategory.set(res.category.id);
        this.selectedSubcategory.set(res.category.subcategory.id);
        this.selectedLevel.set(res.level);
        this.objectivesList.set(res.objectivesSummary);
        this.mustKnowBefore.set(res.mustKnowBefore);
        this.intendedFor.set(res.intendedFor);
        this.price.set(res.price);
      }
    })
  }

    //Files
    selectedFile: File | undefined;
    newImageUrl: SafeUrl | null = null;
    newImageFile: File | null = null;
    showCroper = false;

    onSelectImage(event: FileSelectEvent) {
      this.selectedFile = event.currentFiles[0];
      this.showCroper = true;
    }
  
    upload() {
      this.courseService.editCourseThumbnailPicture(this.courseId(), this.newImageFile!).subscribe({
        next: (res : any) => {
          this.course()!.thumbnailUrl = res.thumbnailUrl;
          this.selectedFile = undefined;
          this.toastService.showSuccess("Data successfully saved")
        },
      });
    }
  
    cancel() {
      this.selectedFile = undefined;
      this.newImageUrl = null;
      this.newImageFile = null;
    }

    onImageCropped(event: { file: File | null; url: SafeUrl }) {
      this.newImageUrl = event.url;
      this.newImageFile = event.file;
      this.showCroper = false;
    }
    onCropperExit(){
      this.showCroper = false;
      this.selectedFile = undefined;
    }

    //Lists
    addItem(list: WritableSignal<string[]>, itemToAdd: string){
      list.update((current) => [...current, itemToAdd]);
      this.change();
      this.editCourseDetail();
    }

    removeFrom(list: WritableSignal<string[]>, itemToRemove: string){
        list.update((current) => current.filter((item) => item !== itemToRemove));
        this.change();
        this.editCourseDetail();
    }

    changed = false;
    change(){
      this.changed = true;
    }
    //Requests
    editCourseDetail(){
      if(this.changed === true){
        this.courseService.editCourseDetails(
          this.courseId(), 
          this.subtitle(), 
          this.description(),
          this.selectedLevel()!,
          this.objectivesList(),
          this.mustKnowBefore(),
          this.intendedFor()).subscribe((res) => {
            this.toastService.showSuccess("Data successfully saved");
          });
      }
      this.changed=false;
    }

    editCourse(){
      if(this.changed === true){
        this.courseService.editCourse(
          this.courseId(),
          this.title(), 
          this.selectedCategory(), 
          this.selectedSubcategory()).subscribe((res) => {
            this.toastService.showSuccess("Data successfully saved");
          });
      }
      this.changed=false;
    }

    changeCategory(){
      this.changed = true;
      const subCategory = this.subcategoryNames().find(s => s.name === 'Other');
      this.selectedSubcategory.set(subCategory!.id)
    }

    editPrice(){
      this.financeService.editPrice(this.courseId(), this.price()).subscribe((res) => {
        this.courseService.courses.update((prevCourses) => 
          prevCourses.map(course => course.id === this.courseId() ? {...course, price: this.price()} : course)
        );
        this.toastService.showSuccess("Data successfully saved");
      });
    }

}
