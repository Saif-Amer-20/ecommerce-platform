/**
 * API Client for Arabic E-Commerce Platform
 * Handles all backend communication with proper Arabic support
 */

// API configuration
const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:44521/api/v1';

export interface ApiResponse<T> {
  data?: T;
  message?: string;
  error?: string;
  page?: number;
  pageSize?: number;
}

export interface Product {
  id: number;
  name: string;
  slug: string;
  price: number;
  costPrice?: number;
  description?: string;
  sku: string;
  categoryId?: number;
  isActive: boolean;
  createdAt: string;
}

export interface Category {
  id: number;
  name: string;
  slug: string;
  description?: string;
  parentId?: number;
}

export interface User {
  id: number;
  fullName: string;
  email: string;
  phone: string;
  isActive: boolean;
}

export interface Order {
  id: number;
  userId: number;
  status: string;
  total: number;
  currency: string;
  paymentMethod: string;
  createdAt: string;
}

class ApiClient {
  private baseUrl: string;

  constructor(baseUrl: string = API_BASE_URL) {
    this.baseUrl = baseUrl;
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<ApiResponse<T>> {
    const url = `${this.baseUrl}${endpoint}`;
    const config: RequestInit = {
      headers: {
        'Content-Type': 'application/json',
        'Accept-Language': 'ar',
        ...options.headers,
      },
      ...options,
    };

    try {
      const response = await fetch(url, config);
      const data = await response.json();
      
      if (!response.ok) {
        throw new Error(data.message || 'حدث خطأ في الاتصال');
      }

      return data;
    } catch (error) {
      console.error('API Error:', error);
      throw error;
    }
  }

  // Catalog API
  async getProducts(page = 1, pageSize = 20, search?: string): Promise<ApiResponse<Product[]>> {
    const params = new URLSearchParams({
      page: page.toString(),
      pageSize: pageSize.toString(),
    });
    if (search) params.append('search', search);
    
    return this.request(`/catalog/products?${params.toString()}`);
  }

  async getProduct(id: number): Promise<ApiResponse<Product>> {
    return this.request(`/catalog/products/${id}`);
  }

  async getCategories(): Promise<ApiResponse<Category[]>> {
    return this.request('/catalog/categories');
  }

  async getProductStock(id: number): Promise<ApiResponse<{ productId: number; stockQuantity: number; isAvailable: boolean }>> {
    return this.request(`/catalog/products/${id}/stock`);
  }

  // User API
  async registerUser(userData: {
    fullName: string;
    email: string;
    phone: string;
    password: string;
  }): Promise<ApiResponse<User>> {
    return this.request('/users/register', {
      method: 'POST',
      body: JSON.stringify(userData),
    });
  }

  async loginUser(credentials: {
    email: string;
    password: string;
  }): Promise<ApiResponse<{ user: User; token: string }>> {
    return this.request('/users/login', {
      method: 'POST',
      body: JSON.stringify(credentials),
    });
  }

  async getUserProfile(): Promise<ApiResponse<User>> {
    return this.request('/users/profile', {
      headers: {
        Authorization: `Bearer ${this.getAuthToken()}`,
      },
    });
  }

  // Order API
  async getUserOrders(): Promise<ApiResponse<Order[]>> {
    return this.request('/orders', {
      headers: {
        Authorization: `Bearer ${this.getAuthToken()}`,
      },
    });
  }

  async createOrder(orderData: {
    items: { productId: number; quantity: number }[];
    paymentMethod: string;
    shippingAddress: any;
  }): Promise<ApiResponse<Order>> {
    return this.request('/orders', {
      method: 'POST',
      body: JSON.stringify(orderData),
      headers: {
        Authorization: `Bearer ${this.getAuthToken()}`,
      },
    });
  }

  // Cart API
  async addToCart(productId: number, quantity: number): Promise<ApiResponse<any>> {
    return this.request('/orders/cart', {
      method: 'POST',
      body: JSON.stringify({ productId, quantity }),
      headers: {
        Authorization: `Bearer ${this.getAuthToken()}`,
      },
    });
  }

  // Authentication helpers
  private getAuthToken(): string | null {
    if (typeof window !== 'undefined') {
      return localStorage.getItem('auth_token');
    }
    return null;
  }

  setAuthToken(token: string): void {
    if (typeof window !== 'undefined') {
      localStorage.setItem('auth_token', token);
    }
  }

  clearAuthToken(): void {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('auth_token');
    }
  }
}

export const apiClient = new ApiClient();