import { computed, inject } from "@angular/core";
import { CanMatchFn, Router } from "@angular/router";
import { UserService } from "../../user/services/user.service";
import { User } from "../../user/models/user.model";
import { PurchasedItemsService } from "../../course/services/purchasedItems.service";

export const isStudent: CanMatchFn = async (route, segments) => {
    const router = inject(Router);
    const userService = inject(UserService)
    const purchasedItemsService = inject(PurchasedItemsService);
    let user: User | null = null;
    let purchasedItems = computed(() => purchasedItemsService.purchasedCourses());
    userService.user.subscribe((res) => {
        user = res;
    })
    const courseId = segments[1].path;
    if(user === null)
    {
        await new Promise(resolve => setTimeout(resolve, 2000)); 
    }
    purchasedItemsService.getPurchasedCourses(user!.id);
    if(purchasedItems().find(c => c.id === courseId)){
        return true;
    }
    router.navigate(['/access-denied']);
    return false;
}