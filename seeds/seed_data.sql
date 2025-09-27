-- بيانات أولية بالعربية للمتجر

-- فئات
INSERT INTO categories (id, parent_id, name, slug, description) VALUES
  (1, NULL, 'إلكترونيات', 'electronics', 'أجهزة إلكترونية متنوعة'),
  (2, 1, 'هواتف ذكية', 'smartphones', 'هواتف ذكية حديثة'),
  (3, 1, 'حاسبات محمولة', 'laptops', 'أجهزة حاسوب محمولة');

-- منتجات
INSERT INTO products (id, category_id, name, slug, description, price) VALUES
  (1, 2, 'هاتف ذكي موديل ألف', 'smartphone-a', 'هاتف ذكي بشاشة 6.5 بوصة وذاكرة 128GB', 400000),
  (2, 3, 'حاسوب محمول موديل باء', 'laptop-b', 'حاسوب محمول بمعالج i7 وذاكرة 16GB', 800000);

-- المتغيرات (مثال: اللون والحجم)
INSERT INTO product_variants (id, sku, price, discounted_price, product_id) VALUES
  (1, 'SM-A-RED', 400000, NULL, 1),
  (2, 'SM-A-BLUE', 400000, NULL, 1),
  (3, 'LP-B-15', 800000, 750000, 2);

-- السمات والقيم
INSERT INTO attributes (id, name) VALUES
  (1, 'اللون'),
  (2, 'الحجم');

INSERT INTO attribute_values (id, attribute_id, value) VALUES
  (1, 1, 'أحمر'),
  (2, 1, 'أزرق'),
  (3, 2, '15 بوصة');

-- ربط السمات بالمتغيرات
INSERT INTO product_variant_attributes (product_variant_id, attribute_id, attribute_value_id) VALUES
  (1, 1, 1),
  (2, 1, 2),
  (3, 2, 3);

-- صور المنتجات
INSERT INTO media_assets (id, url, alt, product_id) VALUES
  (1, 'https://via.placeholder.com/300x300.png?text=Phone+Red', 'هاتف أحمر', 1),
  (2, 'https://via.placeholder.com/300x300.png?text=Phone+Blue', 'هاتف أزرق', 1),
  (3, 'https://via.placeholder.com/300x300.png?text=Laptop', 'حاسوب محمول', 2);

-- المدن وأسعار الشحن
INSERT INTO cities (id, name) VALUES
  (1, 'بغداد'),
  (2, 'البصرة'),
  (3, 'أربيل');

INSERT INTO shipping_methods (id, name) VALUES
  (1, 'توصيل عادي'),
  (2, 'توصيل سريع');

INSERT INTO shipping_rates (id, city_id, shipping_method_id, weight_from, weight_to, cost) VALUES
  (1, 1, 1, 0, 5, 5000),
  (2, 1, 2, 0, 5, 8000),
  (3, 2, 1, 0, 5, 7000),
  (4, 3, 1, 0, 5, 6000);

-- كوبون مثال
INSERT INTO coupons (id, code, description, discount_type, discount_value, start_date, end_date, max_usage) VALUES
  (1, 'WELCOME10', 'خصم 10٪ للعميل الجديد', 'percentage', 10, NOW(), NOW() + INTERVAL '30 days', 100);

-- مستخدم مسؤول افتراضي
INSERT INTO users (id, email, password_hash, full_name, is_active) VALUES
  (1, 'admin@example.com', '$argon2id$v=19$memory$iterations$salt$hash', 'المدير', true);

INSERT INTO roles (id, name) VALUES
  (1, 'Admin');

INSERT INTO user_roles (user_id, role_id) VALUES
  (1, 1);