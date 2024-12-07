import { inject, Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";

@Injectable({
    providedIn: 'root',
  })
  export class CanEnterAddAssignment implements CanActivate {
    private allowed = false;
    courseId = ''
  
    router = inject(Router);
  
    setAllowed(value: boolean): void {
      this.allowed = value;
    }
  
    canActivate(): boolean {
      if (this.allowed) {
        this.allowed = false;
        return true;
      }
      this.router.navigate(['/course-edit/' + this.courseId + '/creator']);
      return false;
    }
  }