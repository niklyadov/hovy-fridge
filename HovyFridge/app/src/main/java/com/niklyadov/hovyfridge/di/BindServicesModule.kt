package com.niklyadov.hovyfridge.di

import com.niklyadov.hovyfridge.services.*
import dagger.Binds
import dagger.Module
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent

@Module
@InstallIn(SingletonComponent::class)
abstract class BindServicesModule {
    @Binds
    abstract fun bindFridgesService(serviceImpl: FridgesServiceImpl) : FridgesService

    @Binds
    abstract fun bindProductsService(serviceImpl: ProductsServiceImpl) : ProductsService

    @Binds
    abstract fun bindUpdatesService(serviceImpl: UpdatesServiceImpl) : UpdatesService
}