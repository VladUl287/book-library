import { GenreMutations } from '../common/enums';
import { GetterTree, Module, ActionContext as AC } from 'vuex';
import { Genre } from '@/common/contracts';
import instance from '@/http';
import { GenreState, RootState } from '../common/types';
import { GenreActions } from '../common/enums';

const state: GenreState = {
    genres: [],
}

const getters: GetterTree<GenreState, RootState> = {}

const actions = {
    async [GenreActions.GET_ALL_GENRES]({ commit }: AC<GenreState, RootState>): Promise<void> {
        const result = await instance.get<Genre[]>('genre/getAll')
        commit(GenreMutations.SET_GENRES, result.data);
    }
}

const mutations = {
    [GenreMutations.SET_GENRES](state: GenreState, data: Genre[]): void {
        state.genres = data
    }
}

export const genre: Module<GenreState, RootState> = {
    state,
    getters,
    actions,
    mutations
}