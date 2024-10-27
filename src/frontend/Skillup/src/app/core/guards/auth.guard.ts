import { inject, signal } from "@angular/core"
import { CanMatchFn, Router } from "@angular/router"
import { UserService } from "../../user/services/user.service";
import { User } from "../../user/models/user.model";
import { UserRole } from "../../user/models/user-role.model";
import { firstValueFrom, take } from "rxjs";

export const hasRole: CanMatchFn = async (route, segments) => {
    const router = inject(Router);
    const userService = inject(UserService)
    const user = signal<User | null>(null);
    const requiredRole = route.data?.['requiredRole'];
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