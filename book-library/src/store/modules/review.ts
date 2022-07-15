import { getUrlParams } from '../common/helpers';
import { Review } from './../../common/contracts';
import { ReviewState, ReviewFilter, RootState } from '../common/types';
import { ReviewMutations, ReviewActions } from '../common/enums';
import { GetterTree, Module, ActionContext as AC, ActionTree } from 'vuex';
import instance from '@/http';

const state: ReviewState = {
    reviews: [],
    filters: {} as ReviewFilter
}

const getters: GetterTree<ReviewState, RootState> = {}

const actions: ActionTree<ReviewState, RootState> = {
    async [ReviewActions.GET_ALL_REVIEWS]({ commit }: AC<ReviewState, RootState>): Promise<void> {
        const params = getUrlParams(state.filters);
        const result = await instance.get<Review[]>('review/getAll', { params })
        commit(ReviewMutations.SET_REVIEWS, result.data);
    }
}

const mutations = {
    [ReviewMutations.SET_REVIEWS](state: ReviewState, data: Review[]): void {
        state.reviews = data
    }
}

export const reviews: Module<ReviewState, RootState> = {
    state,
    getters,
    actions,
    mutations
}