import { store } from "..";
import instance from "@/http";
import { Review } from "@/common/contracts";
import { ReviewFilter } from "../common/types";
import { getUrlParams } from "../common/helpers";
import { VuexModule, Mutation, Action, getModule, Module } from "vuex-module-decorators";

@Module({ dynamic: true, store: store, name: 'reviewsModule', preserveState: localStorage.getItem('vuex') !== null })
class ReviewsModule extends VuexModule {
    private _reviews: Review[] = []
    private _filters: ReviewFilter = {}

    get reviews(): Review[] {
        return this._reviews
    }

    @Mutation
    setReviews(reviews: Review[]): void {
        this._reviews = reviews;
    }

    @Action
    async getReviews(): Promise<void> {
        const params = getUrlParams(this._filters);
        const result = await instance.get<Review[]>('review/getAll', { params })
        this.setReviews(result.data);
    }
}

export const reviewsModule = getModule(ReviewsModule, store)