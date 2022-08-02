import { store } from '..';
import instance from '@/http';
import { getFormData } from '../common/helpers';
import { getUrlParams } from '../common/helpers';
import { Collection, CollectionManageBook } from './../../common/contracts';
import { Action, getModule, Module, Mutation, VuexModule } from 'vuex-module-decorators';
import { CollectionFilter } from '../common/types';
import { Guid } from 'guid-typescript';

@Module({ dynamic: true, store: store, name: 'collectionsModule', preserveState: localStorage.getItem('vuex') !== null })
class CollectionsModule extends VuexModule {
    private _collections: Collection[] = [
        { id: Guid.create(), date: new Date(), author: 'ulyanovskiy.01@mail.ru', name: 'Name', description: 'Lorem, ipsum dolor sit amet consectetur adipisicing elit. Odit voluptate cumque, delectus nostrum, quasi doloribus quas quod veritatis asperiores impedit itaque? Labore ab placeat repellat beatae. Dolorum maiores in maxime aliquid commodi fugit quisquam libero reprehenderit laboriosam obcaecati ad illum earum sunt cumque dolor, hic eos adipisci esse non quae dolore molestiae iusto consectetur iure? Repellat cumque harum illo odio magni aliquid laboriosam error, vero eius dolore quaerat ut suscipit autem doloribus? Non, excepturi quam? Laudantium.', views: 23, likes: 245, books: [] }
    ]

    private _userCollections: Collection[] = []
    private _filters: CollectionFilter = {}

    get collections(): Collection[] {
        return this._collections
    }

    get userCollections(): Collection[] {
        return this._userCollections
    }

    @Mutation
    setCollections(collections: Collection[]): void {
        this._collections = collections
    }

    @Mutation
    setUserCollections(collections: Collection[]): void {
        this._userCollections = collections
    }

    @Mutation
    setFilters(filters: CollectionFilter): void {
        this._filters = filters
    }

    @Action
    async GetAllCollections(): Promise<void> {
        const params = getUrlParams(this._filters)
        const result = await instance.get<Collection[]>('collection/getAll', { params })
        this.setCollections(result.data)
    }

    @Action
    async GetUserCollections(): Promise<void> {
        const result = await instance.get<Collection[]>('collection/getUserCollections')
        this.setUserCollections(result.data)
    }

    @Action
    async CreateCollection(collection: Collection): Promise<void> {
        const formData = getFormData(collection)
        await instance.post('collection/create', formData)
    }

    @Action
    async AddBook(manage: CollectionManageBook): Promise<void> {
        const formData = getFormData(manage)
        await instance.put('collection/addBook', formData)
    }

    @Action
    async RemoveBook(manage: CollectionManageBook): Promise<void> {
        const formData = getFormData(manage)
        await instance.put('collection/removeBook', formData)
    }
}

export const collectionsModule = getModule(CollectionsModule, store)