import instance from '@/http';
import { Book } from '@/common/contracts';
import { BooksFilter, BookState, RootState } from '../types';
import { ActionTree, GetterTree, Module, MutationTree, Store } from 'vuex';
import { getUrlParams } from '../helpers';
import { BooksActions, BooksMutations } from '../enums';

const state: BookState = {
    books: [],
    filters: {}
};

const getters: GetterTree<BookState, RootState> = {};

const actions: ActionTree<BookState, RootState> = {
    async [BooksActions.GET_ALL_BOOKS]({ commit }: any): Promise<void> {
        const result = await instance.get<Book[]>('book/getAll')
        await commit('setBooks', result.data);
    },
    async [BooksActions.GET_BOOKS_WITH_FILTERS]({ commit }: any): Promise<void> {
        const params = getUrlParams(state.filters);
        const result = await instance.get<Book[]>('book/getAll', { params })
        await commit('setBooks', result.data);
    }
};

const mutations: MutationTree<BookState> = {
    [BooksMutations.SET_BOOKS](state: BookState, data: Book[]) {
        state.books = data
    },
    [BooksMutations.SET_FILTERS](state: BookState, data: BooksFilter) {
        state.filters = data;
    },
    [BooksMutations.UPDATE_BOOK](state: BookState, data: Book) {
        const index = state.books.findIndex(x => x.id == data.id);
        if (index) {
            state.books[index] = data;
        }
    }
};

export const books: Module<BookState, RootState> = {
    state,
    getters,
    actions,
    mutations
};