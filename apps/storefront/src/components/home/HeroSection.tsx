import React from 'react';
import Link from 'next/link';
import { Button } from '../ui';

export const HeroSection: React.FC = () => {
  return (
    <section className="relative bg-gradient-to-r from-blue-600 to-purple-700 text-white">
      <div className="absolute inset-0 bg-black opacity-20"></div>
      
      <div className="relative max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-24 lg:py-32">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 items-center">
          {/* Content */}
          <div className="space-y-8">
            <div className="space-y-4">
              <h1 className="text-4xl lg:text-6xl font-bold leading-tight">
                مرحباً بكم في
                <span className="block text-yellow-400">المتجر العربي</span>
              </h1>
              <p className="text-xl lg:text-2xl text-blue-100">
                أفضل منصة للتسوق الإلكتروني في المنطقة العربية
              </p>
            </div>
            
            <div className="space-y-4">
              <ul className="space-y-3 text-lg">
                <li className="flex items-center">
                  <CheckIcon className="h-6 w-6 text-green-400 ml-3 flex-shrink-0" />
                  <span>توصيل مجاني للطلبات فوق 50,000 دينار</span>
                </li>
                <li className="flex items-center">
                  <CheckIcon className="h-6 w-6 text-green-400 ml-3 flex-shrink-0" />
                  <span>دفع عند الاستلام أو عبر ZainCash</span>
                </li>
                <li className="flex items-center">
                  <CheckIcon className="h-6 w-6 text-green-400 ml-3 flex-shrink-0" />
                  <span>ضمان الجودة وإرجاع مجاني خلال 14 يوم</span>
                </li>
                <li className="flex items-center">
                  <CheckIcon className="h-6 w-6 text-green-400 ml-3 flex-shrink-0" />
                  <span>خدمة العملاء على مدار 24/7</span>
                </li>
              </ul>
            </div>

            <div className="flex flex-col sm:flex-row gap-4">
              <Button size="lg" className="bg-yellow-400 hover:bg-yellow-500 text-gray-900 font-bold">
                <Link href="/products">تسوق الآن</Link>
              </Button>
              <Button 
                variant="secondary" 
                size="lg" 
                className="bg-transparent border-2 border-white text-white hover:bg-white hover:text-gray-900"
              >
                <Link href="/categories">تصفح الفئات</Link>
              </Button>
            </div>
          </div>

          {/* Image/Illustration */}
          <div className="relative">
            <div className="bg-white/10 backdrop-blur-sm rounded-2xl p-8 shadow-2xl">
              <div className="grid grid-cols-2 gap-4">
                {/* Product Cards */}
                <div className="space-y-4">
                  <ProductPreviewCard 
                    title="الإلكترونيات"
                    price="299,000"
                    color="bg-blue-500"
                  />
                  <ProductPreviewCard 
                    title="الأزياء"
                    price="89,000"
                    color="bg-pink-500"
                  />
                </div>
                <div className="space-y-4 mt-8">
                  <ProductPreviewCard 
                    title="المنزل والحديقة"
                    price="149,000"
                    color="bg-green-500"
                  />
                  <ProductPreviewCard 
                    title="الرياضة"
                    price="199,000"
                    color="bg-orange-500"
                  />
                </div>
              </div>
              
              {/* Floating elements */}
              <div className="absolute -top-4 -left-4 bg-yellow-400 text-gray-900 rounded-full px-4 py-2 font-bold text-sm">
                خصم 25%
              </div>
              <div className="absolute -bottom-4 -right-4 bg-green-400 text-gray-900 rounded-full px-4 py-2 font-bold text-sm">
                توصيل مجاني
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

interface ProductPreviewCardProps {
  title: string;
  price: string;
  color: string;
}

const ProductPreviewCard: React.FC<ProductPreviewCardProps> = ({ title, price, color }) => (
  <div className="bg-white rounded-lg p-4 shadow-md">
    <div className={`${color} h-16 rounded-lg mb-3`}></div>
    <h4 className="font-semibold text-gray-900 text-sm">{title}</h4>
    <p className="text-gray-600 text-xs">{price} د.ع</p>
  </div>
);

const CheckIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="currentColor" viewBox="0 0 20 20">
    <path fillRule="evenodd" d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z" clipRule="evenodd" />
  </svg>
);