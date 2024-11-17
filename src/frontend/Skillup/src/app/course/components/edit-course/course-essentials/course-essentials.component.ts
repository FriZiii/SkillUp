import { Component, computed, inject, input, NgModule, OnInit, Signal, signal, WritableSignal } from '@angular/core';
import { FormArray, FormControl, FormGroup, FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
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
import { single } from 'rxjs';

@Component({
  selector: 'app-course-essentials',
  standalone: true,
  imports: [InputTextModule, ButtonModule, FileUploadModule, SelectModule, FormsModule, FloatLabelModule, PropertiesListComponent],
  templateUrl: './course-essentials.component.html',
  styleUrl: './course-essentials.component.css'
})
export class CourseEssentialsComponent implements OnInit {
  //Services
  courseService = inject(CoursesService)
  courseCategoryService = inject(CategoryService);
  
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
  
  objectivesList = signal(['']);
  mustKnowBefore = signal(['']);
  intendedFor = signal(['']);


  ngOnInit(): void {
    this.courseService.getCourseById(this.courseId()).subscribe({
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
      }
    })
    console.log(this.courseId())
    console.log(this.levels);
  }

    //Files
    selectedFile: File | undefined;
    onSelectImage(event: FileSelectEvent) {
      this.selectedFile = event.currentFiles[0];
    }
  
    upload() {
      
    }
  
    cancel() {
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
          this.intendedFor()).subscribe({
          
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
          this.selectedSubcategory()).subscribe({});
      }
      this.changed=false;
    }

    changeCategory(){
      this.changed = true;
      const subCategory = this.subcategoryNames().find(s => s.name === 'Other');
      this.selectedSubcategory.set(subCategory!.id)
    }

}
