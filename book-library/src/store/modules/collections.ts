import { getFormData } from '../common/helpers';
import { Collection, CollectionManageBook } from './../../common/contracts';
import { CollectionActions, CollectionMutations } from '../common/enums';
import { CollectionState, CollectionFilter } from '../common/types';
import instance from '@/http';
import { RootState } from '../common/types';
import { ActionTree, GetterTree, Module, MutationTree, ActionContext as AC } from 'vuex';
import { getUrlParams } from '../common/helpers';

const state: CollectionState = {
    collections: [],
    userCollections: [],
    filters: {} as CollectionFilter
};

const getters: GetterTree<CollectionState, RootState> = {};

const actions: ActionTree<CollectionState, RootState> = {
    async [CollectionActions.GET_ALL_COLLECTIONS]({ commit }: AC<CollectionState, RootState>): Promise<void> {
        const params = getUrlParams(state.filters);
        const result = await instance.get<Collection[]>('collection/getAll', { params })
        commit(CollectionMutations.SET_COLLECTIONS, result.data);
    },
    async [CollectionActions.GET_USER_COLLECTIONS]({ commit }: AC<CollectionState, RootState>): Promise<void> {
        const result = await instance.get<Collection[]>('collection/getUserCollections')
        commit(CollectionMutations.SET_COLLECTIONS, result.data);
    },
    async [CollectionActions.CREATE_COLLECTION](_, collection: Collection): Promise<void> {
        const formData = getFormData(collection);
        await instance.post('collection/create', formData)
    },
    async [CollectionActions.ADD_BOOK](_, manage: CollectionManageBook): Promise<void> {
        const formData = getFormData(manage);
        const result = await instance.put('collection/addBook', formData)
    },
    async [CollectionActions.REMOVE_BOOK](_, manage: CollectionManageBook): Promise<void> {
        const formData = getFormData(manage);
        const result = await instance.put('collection/removeBook', formData)
    }
};

const mutations: MutationTree<CollectionState> = {
    [CollectionMutations.SET_COLLECTIONS](state: CollectionState, data: Collection[]) {
        state.collections = data
    },
    [CollectionMutations.SET_FILTERS](state: CollectionState, data: CollectionFilter) {
        state.filters = data
    }
};

export const collections: Module<CollectionState, RootState> = {
    state,
    getters,
    actions,
    mutations
}