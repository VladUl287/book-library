import { GenreMutations } from './../enums';
import { GetterTree, Module } from 'vuex';
import { Genre } from '@/common/contracts';
import instance from '@/http';
import { GenreState, RootState } from '../types';
import { GenreActions } from '../enums';

const state: GenreState = {
    genres: [],
};

const getters: GetterTree<GenreState, RootState> = {
    getGenres: (state: GenreState) => state.genres,
};

const actions = {
    async [GenreActions.GET_ALL_GENRES]({ commit }: any): Promise<void> {
        const result = await instance.get<Genre[]>('genre/getAll')
        await commit('setGenres', result.data);
    }
};

const mutations = {
    [GenreMutations.SET_GENRES](state: GenreState, data: Genre[]) {
        state.genres = data
    }
};

export const genre: Module<GenreState, RootState> = {
    state,
    getters,
    actions,
    mutations
};