import { Module, GetterTree, ActionTree, MutationTree, ActionContext as AC } from 'vuex';
import { BookmarkActions, BookmarkMutations } from '../common/enums';
import instance from '@/http';
import { Book } from '@/common/contracts';
import { Guid } from 'guid-typescript';
import { BookmarkState, RootState } from '../common/types';

const state: BookmarkState = {
    bookmarks: []
}

const getters: GetterTree<BookmarkState, RootState> = {
    getBookmarks: (state: BookmarkState) => state.bookmarks
}

const actions: ActionTree<BookmarkState, RootState> = {
    async [BookmarkActions.GET_BOOKMARKS]({ commit }: AC<BookmarkState, RootState>): Promise<void> {
        const result = await instance.get<Book[]>('bookmarks/get')
        commit(BookmarkMutations.SET_BOOKMARKS, result.data);
    },
    async [BookmarkActions.ADD_BOOKMARK](_, bookId: Guid): Promise<void> {
        await instance.post('bookmarks/add/' + bookId)
    },
    async [BookmarkActions.REMOVER_BOOKMARK](_, bookId: Guid): Promise<void> {
        await instance.post('bookmarks/remove/' + bookId)
    }
}

const mutations: MutationTree<BookmarkState> = {
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