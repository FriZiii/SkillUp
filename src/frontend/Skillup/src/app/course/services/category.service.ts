import { HttpClient } from "@angular/common/http";
import { inject, Injectable, Signal, signal } from "@angular/core";
import { Category } from "../models/category.model";
import { catchError, map, throwError } from "rxjs";
import { environment } from "../../../environments/environment";

@Injectable({providedIn: 'root'})
export class CategoryService {
    private httpClient = inject(HttpClient);
    private _categories = signal<Category[]>([]);

    categories : Signal<Category[]>;


    constructor(){
        this.fetchCategories().subscribe((fetchedCategories: Category[]) => {
            this._categories.set(fetchedCategories);
        });  

        this.categories = this._categories.asReadonly();
    }
    
    loadCategories() {
        return this.fetchCategories();
    }

    private fetchCategories(){
        return this.httpClient.get<{categories: Category[]}>(environment.apiUrl + "/Courses/Categories")
        .pipe(
            map((resData) => resData.categories),
            catchError((error) => throwError(() => new Error("Something went wrong with fetching places")))
        )
    }
}