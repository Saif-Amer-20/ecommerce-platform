'use client';

import React, { useEffect, useState } from 'react';
import { Card } from '../ui';
import { apiClient, Category } from '../../lib/api';

export const CategoriesSection: React.FC = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await apiClient.getCategories();
        if (response.data) {
          setCategories(response.data.slice(0, 6)); // Show first 6 categories
        }
      } catch (err) {
        console.error('Error fetching categories:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, []);

  // Default categories for demo if API fails
  const defaultCategories: Category[] = [
    { id: 1, name: 'الإلكترونيات', slug: 'electronics', description: 'أجهزة ذكية وإلكترونيات' },
    { id: 2, name: 'الأزياء', slug: 'fashion', description: 'ملابس ومستلزمات الأزياء' },
    { id: 3, name: 'المنزل والحديقة', slug: 'home-garden', description: 'أدوات منزلية ومستلزمات الحديقة' },
    { id: 4, name: 'الرياضة واللياقة', slug: 'sports', description: 'معدات رياضية ولياقة بدنية' },
    { id: 5, name: 'الجمال والصحة', slug: 'beauty-health', description: 'منتجات التجميل والصحة' },
    { id: 6, name: 'الكتب والثقافة', slug: 'books', description: 'كتب ومواد ثقافية وتعليمية' }
  ];

  const displayCategories = categories.length > 0 ? categories : defaultCategories;

  return (
    <section className="py-16 bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-12">
          <h2 className="text-3xl font-bold text-gray-900 mb-4">تسوق حسب الفئة</h2>
          <p className="text-lg text-gray-600 max-w-2xl mx-auto">
            اكتشف مجموعة واسعة من المنتجات في مختلف الفئات
          </p>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-8">
          {displayCategories.map((category, index) => (
            <CategoryCard key={category.id} category={category} index={index} />
          ))}
        </div>
      </div>
    </section>
  );
};

interface CategoryCardProps {
  category: Category;
  index: number;
}

const CategoryCard: React.FC<CategoryCardProps> = ({ category, index }) => {
  // Color schemes for categories
  const colorSchemes = [
    { bg: 'bg-blue-500', icon: 'bg-blue-100', text: 'text-blue-800' },
    { bg: 'bg-pink-500', icon: 'bg-pink-100', text: 'text-pink-800' },
    { bg: 'bg-green-500', icon: 'bg-green-100', text: 'text-green-800' },
    { bg: 'bg-orange-500', icon: 'bg-orange-100', text: 'text-orange-800' },
    { bg: 'bg-purple-500', icon: 'bg-purple-100', text: 'text-purple-800' },
    { bg: 'bg-indigo-500', icon: 'bg-indigo-100', text: 'text-indigo-800' }
  ];

  const colorScheme = colorSchemes[index % colorSchemes.length];

  const getCategoryIcon = (index: number) => {
    const icons = [
      <ElectronicsIcon key="electronics" className="h-8 w-8" />,
      <FashionIcon key="fashion" className="h-8 w-8" />,
      <HomeIcon key="home" className="h-8 w-8" />,
      <SportsIcon key="sports" className="h-8 w-8" />,
      <BeautyIcon key="beauty" className="h-8 w-8" />,
      <BooksIcon key="books" className="h-8 w-8" />
    ];
    return icons[index % icons.length];
  };

  return (
    <Card className="group cursor-pointer hover:shadow-xl transition-all duration-300 transform hover:-translate-y-1">
      <div className="text-center space-y-4">
        {/* Icon Container */}
        <div className={`${colorScheme.bg} w-20 h-20 rounded-full mx-auto flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-300`}>
          <div className={`${colorScheme.icon} p-3 rounded-full ${colorScheme.text}`}>
            {getCategoryIcon(index)}
          </div>
        </div>

        {/* Category Info */}
        <div className="space-y-2">
          <h3 className="text-xl font-bold text-gray-900 group-hover:text-blue-600 transition-colors">
            {category.name}
          </h3>
          {category.description && (
            <p className="text-gray-600 text-sm">
              {category.description}
            </p>
          )}
        </div>

        {/* Action Button */}
        <div className="pt-4">
          <span className="text-blue-600 font-medium group-hover:text-blue-800 transition-colors">
            تصفح المنتجات ←
          </span>
        </div>
      </div>
    </Card>
  );
};

// Category Icons
const ElectronicsIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M10.5 1.5H8.25A2.25 2.25 0 006 3.75v16.5a2.25 2.25 0 002.25 2.25h7.5A2.25 2.25 0 0018 20.25V3.75a2.25 2.25 0 00-2.25-2.25H13.5m-3 0V3h3V1.5m-3 0h3m-3 18.75h3" />
  </svg>
);

const FashionIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M15.75 10.5V6a3.75 3.75 0 10-7.5 0v4.5m11.356-1.993l1.263 12c.07.665-.45 1.243-1.119 1.243H4.25a1.125 1.125 0 01-1.12-1.243l1.264-12A1.125 1.125 0 015.513 7.5h12.974c.576 0 1.059.435 1.119 1.007zM8.625 10.5a.375.375 0 11-.75 0 .375.375 0 01.75 0zm7.5 0a.375.375 0 11-.75 0 .375.375 0 01.75 0z" />
  </svg>
);

const HomeIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M2.25 12l8.954-8.955c.44-.439 1.152-.439 1.591 0L21.75 12M4.5 9.75v10.125c0 .621.504 1.125 1.125 1.125H9.75v-4.875c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125V21h4.125c.621 0 1.125-.504 1.125-1.125V9.75M8.25 21h8.25" />
  </svg>
);

const SportsIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M3.75 13.5l10.5-11.25L12 10.5h8.25L9.75 21.75 12 13.5H3.75z" />
  </svg>
);

const BeautyIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M21 8.25c0-2.485-2.099-4.5-4.688-4.5-1.935 0-3.597 1.126-4.312 2.733-.715-1.607-2.377-2.733-4.313-2.733C5.1 3.75 3 5.765 3 8.25c0 7.22 9 12 9 12s9-4.78 9-12z" />
  </svg>
);

const BooksIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M12 6.042A8.967 8.967 0 006 3.75c-1.052 0-2.062.18-3 .512v14.25A8.987 8.987 0 016 18c2.305 0 4.408.867 6 2.292m0-14.25a8.966 8.966 0 016-2.292c1.052 0 2.062.18 3 .512v14.25A8.987 8.987 0 0118 18a8.967 8.967 0 00-6 2.292m0-14.25v14.25" />
  </svg>
);