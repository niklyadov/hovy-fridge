package com.niklyadov.hovyfridge.di

import com.niklyadov.hovyfridge.data.repository.FridgesRepository
import com.niklyadov.hovyfridge.data.repository.FridgesRepositoryImpl
import com.niklyadov.hovyfridge.data.repository.ProductsRepository
import com.niklyadov.hovyfridge.data.repository.ProductsRepositoryImpl
import dagger.Binds
import dagger.Module
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent

@Module
@InstallIn(SingletonComponent::class)
abstract class BindRepositoryModule {
    @Binds
    abstract fun bindProductsRepository(repository: ProductsRepositoryImpl) : ProductsRepository

    @Binds
    abstract fun bindFridgesRepository(repository: FridgesRepositoryImpl) : FridgesRepository
}