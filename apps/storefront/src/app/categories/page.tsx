'use client';

import React, { useEffect, useState } from 'react';
import Link from 'next/link';
import { LoadingSpinner, Card } from '../../components/ui';
import { apiClient, Category } from '../../lib/api';

export default function CategoriesPage() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        setLoading(true);
        const response = await apiClient.getCategories();
        if (response.data) {
          setCategories(response.data);
        }
      } catch (err) {
        setError('حدث خطأ في جلب الفئات');
        console.error('Error fetching categories:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, []);

  if (loading) {
    return (
      <div className="min-h-screen flex justify-center items-center">
        <LoadingSpinner size="lg" />
      </div>
    );
  }

  if (error) {
    return (
      <div className="text-center py-12">
        <p className="text-red-600">{error}</p>
      </div>
    );
  }

  // Default categories for demo if API fails
  const defaultCategories: Category[] = [
    { id: 1, name: 'الإلكترونيات', slug: 'electronics', description: 'أجهزة ذكية وإلكترونيات حديثة' },
    { id: 2, name: 'الأزياء والملابس', slug: 'fashion', description: 'أحدث صيحات الموضة والملابس' },
    { id: 3, name: 'المنزل والحديقة', slug: 'home-garden', description: 'مستلزمات المنزل وأدوات الحديقة' },
    { id: 4, name: 'الرياضة واللياقة', slug: 'sports', description: 'معدات رياضية وأدوات اللياقة البدنية' },
    { id: 5, name: 'الجمال والصحة', slug: 'beauty-health', description: 'منتجات العناية بالجمال والصحة' },
    { id: 6, name: 'الكتب والثقافة', slug: 'books', description: 'كتب ومواد تعليمية وثقافية' },
    { id: 7, name: 'الألعاب والهوايات', slug: 'games', description: 'ألعاب ومستلزمات الهوايات المختلفة' },
    { id: 8, name: 'السيارات والآليات', slug: 'automotive', description: 'قطع غيار ومستلزمات السيارات' }
  ];

  const displayCategories = categories.length > 0 ? categories : defaultCategories;

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
      {/* Header */}
      <div className="text-center mb-12">
        <h1 className="text-3xl font-bold text-gray-900 mb-4">تصفح حسب الفئة</h1>
        <p className="text-lg text-gray-600 max-w-2xl mx-auto">
          اختر الفئة التي تهمك واكتشف مجموعة واسعة من المنتجات المختارة بعناية
        </p>
      </div>

      {/* Categories Grid */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8">
        {displayCategories.map((category, index) => (
          <CategoryCard key={category.id} category={category} index={index} />
        ))}
      </div>

      {/* Empty State */}
      {displayCategories.length === 0 && (
        <div className="text-center py-12">
          <p className="text-gray-500">لا توجد فئات متاحة حالياً</p>
        </div>
      )}
    </div>
  );
}

interface CategoryCardProps {
  category: Category;
  index: number;
}

const CategoryCard: React.FC<CategoryCardProps> = ({ category, index }) => {
  // Color schemes for categories
  const colorSchemes = [
    { bg: 'bg-blue-500', icon: 'bg-blue-100', text: 'text-blue-800', hover: 'hover:bg-blue-600' },
    { bg: 'bg-pink-500', icon: 'bg-pink-100', text: 'text-pink-800', hover: 'hover:bg-pink-600' },
    { bg: 'bg-green-500', icon: 'bg-green-100', text: 'text-green-800', hover: 'hover:bg-green-600' },
    { bg: 'bg-orange-500', icon: 'bg-orange-100', text: 'text-orange-800', hover: 'hover:bg-orange-600' },
    { bg: 'bg-purple-500', icon: 'bg-purple-100', text: 'text-purple-800', hover: 'hover:bg-purple-600' },
    { bg: 'bg-indigo-500', icon: 'bg-indigo-100', text: 'text-indigo-800', hover: 'hover:bg-indigo-600' },
    { bg: 'bg-red-500', icon: 'bg-red-100', text: 'text-red-800', hover: 'hover:bg-red-600' },
    { bg: 'bg-teal-500', icon: 'bg-teal-100', text: 'text-teal-800', hover: 'hover:bg-teal-600' }
  ];

  const colorScheme = colorSchemes[index % colorSchemes.length];

  const getCategoryIcon = (index: number) => {
    const icons = [
      <ElectronicsIcon key="electronics" className="h-8 w-8" />,
      <FashionIcon key="fashion" className="h-8 w-8" />,
      <HomeIcon key="home" className="h-8 w-8" />,
      <SportsIcon key="sports" className="h-8 w-8" />,
      <BeautyIcon key="beauty" className="h-8 w-8" />,
      <BooksIcon key="books" className="h-8 w-8" />,
      <GamesIcon key="games" className="h-8 w-8" />,
      <AutomotiveIcon key="automotive" className="h-8 w-8" />
    ];
    return icons[index % icons.length];
  };

  return (
    <Link href={`/categories/${category.slug}`}>
      <Card className="group cursor-pointer hover:shadow-xl transition-all duration-300 transform hover:-translate-y-1">
        <div className="text-center space-y-4">
          {/* Icon Container */}
          <div className={`${colorScheme.bg} ${colorScheme.hover} w-20 h-20 rounded-full mx-auto flex items-center justify-center mb-6 group-hover:scale-110 transition-all duration-300`}>
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

          {/* Action Indicator */}
          <div className="pt-4">
            <span className="inline-flex items-center text-blue-600 font-medium group-hover:text-blue-800 transition-colors">
              تصفح المنتجات
              <ArrowIcon className="h-4 w-4 mr-1 group-hover:translate-x-1 transition-transform" />
            </span>
          </div>
        </div>
      </Card>
    </Link>
  );
};

// Icons
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

const GamesIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M14.25 6.087c0-.355.186-.676.401-.959.221-.29.349-.634.349-1.003 0-1.036-1.007-1.875-2.25-1.875s-2.25.84-2.25 1.875c0 .369.128.713.349 1.003.215.283.401.604.401.959v0a.64.64 0 01-.657.643 48.39 48.39 0 01-4.163-.3c.186 1.613.293 3.25.315 4.907a.656.656 0 01-.658.663v0c-.355 0-.676-.186-.959-.401a1.647 1.647 0 00-1.003-.349c-1.036 0-1.875 1.007-1.875 2.25s.84 2.25 1.875 2.25c.369 0 .713-.128 1.003-.349.283-.215.604-.401.959-.401v0c.31 0 .555.26.532.57a48.039 48.039 0 01-.303 4.163c1.613-.185 3.25-.293 4.907-.315a.656.656 0 01.663.658v0c0 .355-.186.676-.401.959-.221.29-.349.634-.349 1.003 0 1.036 1.007 1.875 2.25 1.875s2.25-.839 2.25-1.875c0-.369-.128-.713-.349-1.003a1.647 1.647 0 01-.401-.959v0c0-.31.26-.555.57-.532a48.1 48.1 0 004.163.303c-.185-1.613-.293-3.25-.315-4.907a.656.656 0 01.658-.663v0c.355 0 .676.186.959.401.29.221.634.349 1.003.349 1.036 0 1.875-1.007 1.875-2.25s-.839-2.25-1.875-2.25c-.369 0-.713.128-1.003.349a1.651 1.651 0 01-.959.401v0a.656.656 0 01-.663-.658 48.422 48.422 0 01.315-4.907 1.645 1.645 0 01-.303-4.163.656.656 0 01.532-.57z" />
  </svg>
);

const AutomotiveIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M8.25 18.75a1.5 1.5 0 01-3 0 1.5 1.5 0 013 0zM19.5 18.75a1.5 1.5 0 01-3 0 1.5 1.5 0 013 0zM3 7.5a.75.75 0 00-.75.75v1.5c0 .414.336.75.75.75h6.75V7.5H3zM4.5 12.75a.75.75 0 01.75-.75h13.5a.75.75 0 01.75.75v2.25a.75.75 0 01-.75.75H5.25a.75.75 0 01-.75-.75v-2.25zm7.5-9.75v6m-3-3l3-3 3 3" />
  </svg>
);

const ArrowIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M15.75 19.5L8.25 12l7.5-7.5" />
  </svg>
);