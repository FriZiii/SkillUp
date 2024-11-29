import { computed, inject, signal } from "@angular/core"
import { CanMatchFn, Router } from "@angular/router"
import { UserService } from "../../user/services/user.service";
import { User } from "../../user/models/user.model";
import { UserRole } from "../../user/models/user-role.model";
import { CoursesService } from "../../course/services/course.service";
import { CourseListItem } from "../../course/models/course.model";

export const hasRole: CanMatchFn = async (route, segments) => {
    const router = inject(Router);
    const userService = inject(UserService)
    const user = signal<User | null>(null);
    const requiredRole = route.data?.['requiredRole'];
    await new Promise(resolve => setTimeout(resolve, 1000)); 
    userService.user.subscribe((data) => {
        next: {
            user.set(data);
        }
    })
    
    if(user()?.isInRole(requiredRole)){
        return true;
    }
    router.navigate(['/access-denied']);
    return false;
}

export const isAuthor: CanMatchFn = async (route, segments) => {
    console.log('lol');
    const router = inject(Router);
    const userService = inject(UserService)
    const courseSerivce = inject(CoursesService)
    const user = signal<User | null>(null);
    const courseId = segments[1].path;
    const course = computed(() => courseSerivce.courses().find(c => c.id === courseId))
    await new Promise(resolve => setTimeout(resolve, 1000)); 
    userService.user.subscribe((data) => {
        next: {
            user.set(data);
        }
    })
    
    if(user()?.isInRole(UserRole.Instructor) && user()?.id === course()?.authorId){
        return true;
    }
    
    router.navigate(['/access-denied']);
    return false;
}