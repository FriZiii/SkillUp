import { computed, inject } from "@angular/core"
import { CanMatchFn, Router } from "@angular/router"
import { UserService } from "../../user/services/user.service";
import { UserRole } from "../../user/models/user-role.model";
import { CoursesService } from "../../course/services/course.service";
import { User } from "../../user/models/user.model";

export const hasRole: CanMatchFn = async (route, segments) => {
    const router = inject(Router);
    const userService = inject(UserService)
    let user: User | null = null;
    userService.user.subscribe((res) => {
        user = res;
    })
    const requiredRole = route.data?.['requiredRole'];
    if(user === null)
    {
        await new Promise(resolve => setTimeout(resolve, 2000)); 
    }
    
    if(user!.isInRole(requiredRole)){
        return true;
    }
    router.navigate(['/access-denied']);
    return false;
}

export const isAuthor: CanMatchFn = async (route, segments) => {
    const router = inject(Router);
    const userService = inject(UserService)
    const courseSerivce = inject(CoursesService)
    let user: User | null = null;
    userService.user.subscribe((res) => {
        user = res;
    })
    const courseId = segments[1].path;
    const course = computed(() => courseSerivce.courses().find(c => c.id === courseId))
    if(user === null || course() === null)
    {
        await new Promise(resolve => setTimeout(resolve, 2000)); 
    }

    if(user!.isInRole(UserRole.Instructor) && user!.id === course()?.authorId){
        return true;
    }
    router.navigate(['/access-denied']);
    return false;
}

export const isSignedIn: CanMatchFn = async (route, segments) => {
    const router = inject(Router);
    const userService = inject(UserService)
    let user: User | null = null;
    userService.user.subscribe((res) => {
        user = res;
    })
    if(user === null)
    {
        await new Promise(resolve => setTimeout(resolve, 2000)); 
    }
    
    if(user !== null){
        return true;
    }
    router.navigate(['/access-denied']);
    return false;
}