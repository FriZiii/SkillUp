import { HttpClient } from "@angular/common/http";
import { inject, Injectable, Signal, signal } from "@angular/core";
import { Category } from "../models/category.model";
import { catchError, map, tap, throwError } from "rxjs";
import { environment } from "../../../environments/environment";

@Injectable({providedIn: 'root'})
export class CategoryService {
    private httpClient = inject(HttpClient);
    private _categories = signal<Category[]>([]);

    //categories : Signal<Category[]>;
    categories = this._categories.asReadonly();


    constructor(){
        this.fetchCategories().subscribe({
            next: (data) => {
                this._categories = signal(data);
            }
        })
    }
    
    loadCategories() {
        return this.fetchCategories();
    }

    private fetchCategories(){
        return this.httpClient
        .get<Category[]>(environment.apiUrl + "/Courses/Categories")
        .pipe(
            map((resData) => resData),
            catchError((error) => throwError(() => new Error("Something went wrong with fetching places")))
        )
    }
}