import Vuex, { Store, StoreOptions, useStore as baseUseStore } from 'vuex';
import { books } from './modules/books';
import createPersistedState from "vuex-persistedstate";
import { AuthState, BookmarkState, BookState, GenreState, RootState } from './types';
import { genre } from './modules/genre';
import { auth } from './modules/auth';
import { bookmarks } from './modules/bookmarks';
import { InjectionKey } from '@vue/runtime-dom';

export const storeKey: InjectionKey<Store<RootState>> = Symbol()

const store: StoreOptions<RootState> = {
  state: {
    books: {} as BookState,
    genres: {} as GenreState,
    bookmarks: {} as BookmarkState,
    auth: {} as AuthState
  },
  modules: {
    auth,
    books,
    genre,
    bookmarks,
  },
  plugins: [createPersistedState()]
}

export const useStore = () => {
  return baseUseStore(storeKey);
}

export default new Vuex.Store<RootState>(store);