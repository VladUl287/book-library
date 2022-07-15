import Vuex, { Store, StoreOptions, useStore as baseUseStore } from 'vuex';
import { books } from './modules/books';
import createPersistedState from "vuex-persistedstate";
import { AuthState, BookmarkState, BookState, CollectionState, GenreState, RootState, ReviewState } from './common/types';
import { genre } from './modules/genre';
import { auth } from './modules/auth';
import { bookmarks } from './modules/bookmarks';
import { InjectionKey } from '@vue/runtime-dom';
import { collections } from './modules/collections';
import { reviews } from './modules/review';

export const storeKey: InjectionKey<Store<RootState>> = Symbol()

const store: StoreOptions<RootState> = {
  state: {},
  modules: {
    auth,
    books,
    genre,
    reviews,
    bookmarks,
    collections
  },
  plugins: [createPersistedState()]
}

export const useStore = () => {
  return baseUseStore(storeKey);
}

export default new Vuex.Store(store);