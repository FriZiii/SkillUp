import { HttpClient } from "@angular/common/http";
import { inject } from "@angular/core";
import { AddCourse } from "../models/course.model";

export class CoursesService {
    private httpClient = inject(HttpClient);

    addCourse(course: AddCourse){
        
    }
}