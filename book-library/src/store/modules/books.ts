import { Guid } from 'guid-typescript';
import instance from '@/http';
import { Book } from '@/common/contracts';
import { BooksFilter, BookState, RootState } from '../common/types';
import { ActionContext as AC, ActionTree, GetterTree, Module, MutationTree } from 'vuex';
import { getUrlParams } from '../common/helpers';
import { BooksActions, BooksMutations } from '../common/enums';

const state: BookState = {
    books: [],
    filters: {}
};

const getters: GetterTree<BookState, RootState> = {
    getBooks: (state: BookState) => state.books,
    getFilters: (state: BookState) => state.filters
};

const actions: ActionTree<BookState, RootState> = {
    async [BooksActions.GET_ALL_BOOKS]({ commit }: AC<BookState, RootState>): Promise<void> {
        const result = await instance.get<Book[]>('book/getAll')
        commit(BooksMutations.SET_BOOKS, result.data);
    },
    async [BooksActions.GET_BOOKS_WITH_FILTERS]({ commit }: AC<BookState, RootState>): Promise<void> {
        const params = getUrlParams(state.filters);
        const result = await instance.get<Book[]>('book/getAll', { params })
        commit(BooksMutations.SET_BOOKS, result.data);
    },
    async [BooksActions.GET_BOOKS_BY_COLLECTION]({ commit }: AC<BookState, RootState>, collectionId: Guid): Promise<void> {
        const params = getUrlParams(state.filters);
        const result = await instance.get<Book[]>('book/getByCollection/' + collectionId, { params })
        commit(BooksMutations.SET_BOOKS, result.data);
    },
    async [BooksActions.GET_BOOKS_BY_AUTHOR]({ commit }: AC<BookState, RootState>, authorId: Guid): Promise<void> {
        const params = getUrlParams(state.filters);
        const result = await instance.get<Book[]>('book/getByAuthor/' + authorId, { params })
        commit(BooksMutations.SET_BOOKS, result.data);
    }
};

const mutations: MutationTree<BookState> = {
    [BooksMutations.SET_BOOKS](state: BookState, data: Book[]) {
        state.books = data
    },
    [BooksMutations.SET_FILTERS](state: BookState, data: BooksFilter) {
        state.filters = data;
    }
};

export const books: Module<BookState, RootState> = {
    state,
    getters,
    actions,
    mutations
};