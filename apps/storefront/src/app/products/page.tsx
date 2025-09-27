'use client';

import React, { useEffect, useState } from 'react';
import { LoadingSpinner, Card, Button } from '../../components/ui';
import { apiClient, Product } from '../../lib/api';

export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [search, setSearch] = useState('');
  const [page, setPage] = useState(1);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        setLoading(true);
        const response = await apiClient.getProducts(page, 12, search || undefined);
        if (response.data) {
          setProducts(response.data);
        }
      } catch (err) {
        setError('حدث خطأ في جلب المنتجات');
        console.error('Error fetching products:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, [page, search]);

  const handleSearch = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setPage(1); // Reset to first page when searching
  };

  if (loading && products.length === 0) {
    return (
      <div className="min-h-screen flex justify-center items-center">
        <LoadingSpinner size="lg" />
      </div>
    );
  }

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
      {/* Header */}
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-gray-900 mb-4">جميع المنتجات</h1>
        
        {/* Search */}
        <form onSubmit={handleSearch} className="max-w-md">
          <div className="flex">
            <input
              type="text"
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              placeholder="ابحث عن المنتجات..."
              className="flex-1 px-4 py-2 border border-gray-300 rounded-r-lg focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
            />
            <Button type="submit" className="rounded-l-lg rounded-r-none">
              بحث
            </Button>
          </div>
        </form>
      </div>

      {/* Error State */}
      {error && (
        <div className="text-center py-12">
          <div className="text-red-600 mb-4">{error}</div>
          <Button onClick={() => window.location.reload()}>
            إعادة المحاولة
          </Button>
        </div>
      )}

      {/* Products Grid */}
      {!error && (
        <>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
            {products.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>

          {/* Empty State */}
          {products.length === 0 && !loading && (
            <div className="text-center py-12">
              <div className="text-gray-500 mb-4">
                {search ? `لم نجد منتجات تطابق "${search}"` : 'لا توجد منتجات متاحة حالياً'}
              </div>
              {search && (
                <Button 
                  onClick={() => {
                    setSearch('');
                    setPage(1);
                  }}
                  variant="secondary"
                >
                  عرض جميع المنتجات
                </Button>
              )}
            </div>
          )}

          {/* Loading more */}
          {loading && products.length > 0 && (
            <div className="flex justify-center mt-8">
              <LoadingSpinner />
            </div>
          )}
        </>
      )}
    </div>
  );
}

interface ProductCardProps {
  product: Product;
}

const ProductCard: React.FC<ProductCardProps> = ({ product }) => {
  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('ar-IQ', {
      style: 'currency',
      currency: 'IQD',
      minimumFractionDigits: 0,
    }).format(price);
  };

  const handleAddToCart = async () => {
    try {
      await apiClient.addToCart(product.id, 1);
      // Show success message (you can implement a toast system)
      alert('تم إضافة المنتج إلى السلة بنجاح!');
    } catch (error) {
      console.error('Error adding to cart:', error);
      alert('حدث خطأ في إضافة المنتج إلى السلة');
    }
  };

  return (
    <Card className="group cursor-pointer hover:shadow-lg transition-shadow duration-300">
      <div className="aspect-square bg-gray-200 rounded-lg mb-4 flex items-center justify-center">
        <ImageIcon className="h-12 w-12 text-gray-400" />
      </div>
      
      <div className="space-y-3">
        <h3 className="font-semibold text-lg text-gray-900 group-hover:text-blue-600 transition-colors">
          {product.name}
        </h3>
        
        {product.description && (
          <p className="text-gray-600 text-sm line-clamp-2">
            {product.description}
          </p>
        )}
        
        <div className="flex justify-between items-center">
          <span className="text-xl font-bold text-blue-600">
            {formatPrice(product.price)}
          </span>
          <span className="text-xs text-gray-500 bg-gray-100 px-2 py-1 rounded">
            {product.sku}
          </span>
        </div>
        
        <div className="flex space-x-2 space-x-reverse">
          <Button 
            className="flex-1"
            onClick={handleAddToCart}
          >
            إضافة للسلة
          </Button>
          <Button 
            variant="secondary" 
            className="px-3"
            onClick={() => {
              // Navigate to product details (implement later)
              console.log('View product:', product.id);
            }}
          >
            <EyeIcon className="h-4 w-4" />
          </Button>
        </div>
      </div>
    </Card>
  );
};

// Icons
const ImageIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="m2.25 15.75 5.159-5.159a2.25 2.25 0 0 1 3.182 0l5.159 5.159m-1.5-1.5 1.409-1.409a2.25 2.25 0 0 1 3.182 0l2.909 2.909m-18 3.75h16.5a1.5 1.5 0 0 0 1.5-1.5V6a1.5 1.5 0 0 0-1.5-1.5H3.75A1.5 1.5 0 0 0 2.25 6v12a1.5 1.5 0 0 0 1.5 1.5Zm10.5-11.25h.008v.008h-.008V8.25Zm.375 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Z" />
  </svg>
);

const EyeIcon: React.FC<{ className: string }> = ({ className }) => (
  <svg className={className} fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
    <path strokeLinecap="round" strokeLinejoin="round" d="M2.036 12.322a1.012 1.012 0 010-.639C3.423 7.51 7.36 4.5 12 4.5c4.638 0 8.573 3.007 9.963 7.178.07.207.07.431 0 .639C20.577 16.49 16.64 19.5 12 19.5c-4.638 0-8.573-3.007-9.963-7.178z" />
    <path strokeLinecap="round" strokeLinejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
  </svg>
);