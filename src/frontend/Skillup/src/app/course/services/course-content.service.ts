import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { environment } from "../../../environments/environment";
import { map, tap } from "rxjs";
import { ToastHandlerService } from "../../core/services/toast-handler.service";
import { AssetType, Attachment, Section } from "../models/course-content.model";

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
              response.type = response.type as AssetType;
              return response;
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
              isPublished: response.isPublished,
              elements: []
            }
            ])
          })
        );
    }

    deleteSection(sectionId: string){
      const prevSections = this.sections();
      return this.httpClient
        .delete(environment.apiUrl + '/Courses/Sections/' + sectionId)
        .pipe(
          tap(() => {
            this.sections.set(prevSections
              .filter(p => p.id !== sectionId)
              .map((section, index) => ({ ...section, index })));
          })
        );
    }

    updateSection(sectionId: string, sectionTitle: string, sectionIsPublished: boolean){
      return this.httpClient
        .put(environment.apiUrl + '/Courses/Sections/' + sectionId, {title: sectionTitle, isPublished: sectionIsPublished})
        .pipe(
          tap(() => {
            this.sections.update((prevSections) => 
            prevSections.map(section => section.id === sectionId ? {...section, title: sectionTitle, isPublished: sectionIsPublished} : section));
          })
        );
    }

    updateSectionIndex(sectionId: string, newIndex: number){
      return this.httpClient
        .put<Section[]>(environment.apiUrl + '/Courses/Sections/' + sectionId + '/' + newIndex, {})
        .pipe(
          tap((response: Section[]) => {
            this.sections.set(response);
          })
        );
    }


    //Elements
    addElement(sectionId: string, assetType: AssetType, elementTitle: string, elementDescription: string, elementFree: boolean) {
      return this.httpClient
        .post<any>(environment.apiUrl + '/Courses/Elements/' + assetType + '/' + sectionId, {title: elementTitle, description: elementDescription, isFree: elementFree})
        .pipe(
          tap((response) => {
            const updatedSections = this.sections().map(section => {
              if (section.id === sectionId) {
                return {
                  ...section,
                  elements: [
                    ...section.elements,
                    {
                      id: response.id,
                      title: response.title,
                      description: response.description,
                      type: response.type,
                      index: response.index,
                      isFree: response.isFree,
                      hasAsset: response.hasAsset,
                      attachments: response.attachments
                    }
                  ]
                };
              }
              return section;
            });
              this.sections.set(updatedSections);
          })
        );
    }

    deleteElement(sectionId: string, elementId: string){
      return this.httpClient
        .delete(environment.apiUrl + '/Courses/Elements/' + elementId)
        .pipe(
          tap(() => {
            this.sections.update((prevSections) =>
              prevSections.map(section => {
                if (section.id === sectionId) {
                  const updatedElements = section.elements
                  .filter(element => element.id !== elementId)
                  .map((element, index) => ({ ...element, index }));
                  return { ...section, elements: updatedElements };
                  }
                return section;
              })
            );
          })
        );
    }

    updateElement(sectionId: string, elementId: string, elementTitle: string, elementDescription: string, elementFree: boolean){
      return this.httpClient
        .put(environment.apiUrl + '/Courses/Elements/' + elementId, {title: elementTitle, description: elementDescription, isFree:elementFree})
        .pipe(
          tap(() => {
            this.sections.update((prevSections) =>
              prevSections.map(section => {
                if (section.id === sectionId) {
                  const updatedElements = section.elements.map(element =>
                    element.id === elementId ? { ...element, title: elementTitle, description: elementDescription, isFree: elementFree } : element
                  );
                  return { ...section, elements: updatedElements };
                }
                return section;
              })
            );
          })
        );
    }

    updateElementIndex(sectionId: string, elementId: string, newIndex: number){
      return this.httpClient
        .put<Section>(environment.apiUrl + '/Courses/Elements/' + elementId + '/' + newIndex, {})
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
          })
        );
    }

    addAttachment(elementId: string, articleFile: File) {
      const formData = new FormData();
      formData.append('file', articleFile);
      return this.httpClient.post<any>(environment.apiUrl + '/Courses/Elements/' + elementId + '/Attachments', formData);
  }

  getAttachment(attachmentId: string) {
    return this.httpClient.get(environment.apiUrl + '/Courses/Elements/Attachments/' + attachmentId, {
      responseType: 'blob'
    });
    /* observe: 'response', */
}

  deleteAttachment(attachmentId: string) {
    return this.httpClient.delete(environment.apiUrl + '/Courses/Elements/Attachments/' + attachmentId);
  }

  getAttachmentsByElementId(elementId: string){
    return this.httpClient.get<Attachment[]>(environment.apiUrl + '/Courses/Elements/' + elementId + '/Attachments');
  }

  getAttachmentsByCoruseId(courseId: string){
    return this.httpClient.get<Attachment[]>(environment.apiUrl + '/Courses/' + courseId + '/Elements/Attachments');
  }
}