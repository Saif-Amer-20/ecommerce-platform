import { test, expect } from '@playwright/test';

// سيناريو End-to-End لعملية شراء عبر الدفع عند الاستلام (COD)
// هذا السيناريو يوضح خطوات متسلسلة: تصفح، اختيار منتج، إضافة إلى السلة، متابعة الدفع وتأكيد الطلب.

test('تدفق شراء COD', async ({ page }) => {
  // الذهاب إلى الصفحة الرئيسية للمتجر
  await page.goto('/');
  // اضغط على أول رابط منتج (هذا مجرد مثال؛ يجب ضبط المحددات حسب العناصر الفعلية)
  // TODO: تحديد selectors المناسبة للمنتج والسلة
  await page.getByText('هاتف ذكي').click();
  await page.getByRole('button', { name: 'أضف إلى السلة' }).click();

  // انتقل إلى السلة
  await page.getByRole('link', { name: 'السلة' }).click();
  // تأكد من وجود العنصر في السلة
  await expect(page.getByText('هاتف ذكي')).toBeVisible();

  // متابعة إلى Checkout
  await page.getByRole('button', { name: 'الانتقال للدفع' }).click();
  // أدخل عنوان الشحن
  await page.getByLabel('العنوان').fill('شارع الرشيد, بغداد');
  await page.getByLabel('المدينة').selectOption('1');
  // اختر COD
  await page.getByLabel('طريقة الدفع').selectOption('cod');
  // تأكيد الطلب
  await page.getByRole('button', { name: 'تأكيد الطلب' }).click();
  // تحقّق من رسالة النجاح
  await expect(page.getByText('تم إنشاء طلبك')).toBeVisible();
});