import { Module } from 'vuex';
import { BookmarkActions, BookmarkMutations } from './../enums';
import instance from '@/http';
import { Book } from '@/common/contracts';
import { Guid } from 'guid-typescript';
import { BookmarkState, RootState } from '../types';

const state: BookmarkState = {
    bookmarks: []
}

const getters = {
    getBookmarks: (state: BookmarkState) => state.bookmarks,
}

const actions = {
    async [BookmarkActions.GET_BOOKMARKS]({ commit }: any): Promise<void> {
        const result = await instance.get<Book[]>('bookmark/get')
        await commit(BookmarkMutations.SET_BOOKMARKS, result.data);
    },
    async [BookmarkActions.ADD_BOOKMARK](_: any, bookId: Guid): Promise<void> {
        await instance.post('bookmarks/add/' + bookId.toString())
    },
    async [BookmarkActions.REMOVER_BOOKMARK](_: any, bookId: Guid): Promise<void> {
        await instance.post('bookmarks/remove/' + bookId.toString())
    }
}

const mutations = {
    [BookmarkMutations.SET_BOOKMARKS](state: BookmarkState, data: Book[]) {
        state.bookmarks = data
    },
}

export const bookmarks: Module<BookmarkState, RootState> = {
    state,
    getters,
    actions,
    mutations
}