package com.niklyadov.hovyfridge.di

import android.content.Context
import android.os.Build
import androidx.preference.PreferenceManager
import com.niklyadov.hovyfridge.App
import com.niklyadov.hovyfridge.data.api.ApiInterceptor
import com.niklyadov.hovyfridge.data.api.RetrofitServices
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

@Module
@InstallIn(SingletonComponent::class)
class ApiModule {

    private fun getLogger() :HttpLoggingInterceptor {
        val logging = HttpLoggingInterceptor()
        logging.setLevel(HttpLoggingInterceptor.Level.BODY)
        return logging
    }

    private fun buildClient (authToken : String) =  OkHttpClient.Builder().apply {
        addInterceptor(getLogger())
        addInterceptor(ApiInterceptor(
            authToken,
            "HovyFridge",
            "1.0",
            "${Build.BRAND} ${Build.MODEL}, ${Build.BOARD} ${Build.DEVICE}, ${Build.HOST} ${Build.PRODUCT}"
        ))
    }.build()

    @Provides
    fun provideAppApi(@ApplicationContext context: Context)
            : RetrofitServices
    {
        val sp = PreferenceManager.getDefaultSharedPreferences(context)
        var apiUrl = sp.getString("api_url", null) ?: App.DEFAULT_API_URL
        if(!apiUrl.matches(Regex("(https?://.*):(\\d*)\\/?(.*)"))) {
            apiUrl = App.DEFAULT_API_URL
        }

        val authApiToken = sp.getString("api_token", "no-provided") ?: "not-provided"

        val retrofit = Retrofit.Builder()
            .baseUrl(apiUrl)
            .client(buildClient(authApiToken))
            .addConverterFactory(GsonConverterFactory.create())
            .build();

        return retrofit.create(RetrofitServices::class.java);
    }
}