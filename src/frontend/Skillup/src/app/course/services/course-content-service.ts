import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { environment } from "../../../environments/environment";
import { catchError, map, Observable, tap, throwError } from "rxjs";
import { ToastHandlerService } from "../../core/services/toast-handler.service";
import { ElementType, Section } from "../models/course-content.model";

@Injectable({ providedIn: 'root' })
export class CourseContentService {
    private toastService = inject(ToastHandlerService);
    private httpClient = inject(HttpClient);
    sections = signal<Section[]>([]);

    getSectionsByCourseId(courseId: string): void {
        this.fetchSectionsByCourseId(courseId)
        .subscribe((res) => {
          this.sections.set(res);
        });
      }

    private fetchSectionsByCourseId(courseId: string) {
        return this.httpClient
          .get<any>(environment.apiUrl + '/Courses/Sections/' + courseId)
          .pipe(
            map((response) => {
              response.elementType = response.elementType as ElementType;
              console.log(response);
              return response;
            }),
            catchError((error) => {
              this.toastService.showError('Coud not fetch sections');
              return throwError(() => error);
            })
          );
    }

    //Sections
    addSection(courseId: string, sectionTitle: string, index: number) {
      return this.httpClient
        .post<any>(environment.apiUrl + '/Courses/Sections/' + courseId, {title: sectionTitle, index: index})
        .pipe(
          tap((response) => {
            this.sections.set([
            ...this.sections(),
            {
              id: response.id,
              title: response.title,
              index: response.index,
              elements: []
            }
            ])
          }),
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }

    deleteSection(sectionId: string){
      const prevSections = this.sections();
      return this.httpClient
        .delete(environment.apiUrl + '/Courses/Sections/' + sectionId)
        .pipe(
          tap(() => {
            this.sections.set(prevSections.filter(p => p.id !== sectionId));
          }),
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }

    updateSection(sectionId: string, sectionTitle: string){
      return this.httpClient
        .put(environment.apiUrl + '/Courses/Sections/' + sectionId, {title: sectionTitle})
        .pipe(
          tap(() => {
            this.sections.update((prevSections) => 
            prevSections.map(section => section.id === sectionId ? {...section, title: sectionTitle} : section));
          }),
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }

    updateSectionIndex(sectionId: string, newIndex: number){
      return this.httpClient
        .put<Section[]>(environment.apiUrl + '/Courses/Sections/' + sectionId + '/Edit-Index', {index: newIndex})
        .pipe(
          tap((response: Section[]) => {
            this.sections.set(response);
          }),
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }


    //Elements
    private addElement() {
    }

    deleteElement(sectionId: string, elementId: string){
      return this.httpClient
        .delete(environment.apiUrl + '/Courses/Elements/' + elementId)
        .pipe(
          tap(() => {
            this.sections.update((prevSections) =>
              prevSections.map(section => {
                if (section.id === sectionId) {
                  const updatedElements = section.elements.filter(element => element.id !== elementId);
                  return { ...section, elements: updatedElements };
                  }
                return section;
              })
            );
          }),
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }

    updateElement(sectionId: string, elementId: string, elementTitle: string, elementDescription: string){
      return this.httpClient
        .put(environment.apiUrl + '/Courses/Elements/' + elementId, {title: elementTitle, description: elementDescription})
        .pipe(
          tap(() => {
            this.sections.update((prevSections) =>
              prevSections.map(section => {
                if (section.id === sectionId) {
                  const updatedElements = section.elements.map(element =>
                    element.id === elementId ? { ...element, title: elementTitle, description: elementDescription } : element
                  );
                  return { ...section, elements: updatedElements };
                }
                return section;
              })
            );
          }),
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }

    updateElementIndex(sectionId: string, elementId: string, newIndex: number){
      return this.httpClient
        .put<Section>(environment.apiUrl + '/Courses/Elements/' + elementId + '/Edit-Index', {index: newIndex})
        .pipe(
          tap((response: Section) => {
            this.sections.update((prevSections) =>
              prevSections.map(section => {
                if (section.id === sectionId) {
                  return response;
                  }
                return section;
              })
            );
          }),
          catchError((error) => {
            return throwError(() => error);
          })
        );
    }
}