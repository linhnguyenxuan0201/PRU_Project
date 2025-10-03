# Hướng dẫn tạo hệ thống nhặt Coin cho Player trong Unity 6.2

## Danh sách công việc cần thực hiện

### 1. Tạo Script Coin.cs ✅
- [x] Tạo class Coin với các thuộc tính cơ bản
- [x] Thêm hiệu ứng xoay cho coin
- [x] Xử lý va chạm với player
- [x] Tự hủy khi được nhặt
- [x] Thêm âm thanh/hiệu ứng khi nhặt (tùy chọn)

### 2. Cập nhật Player.cs ✅
- [x] Thêm biến đếm số coin đã nhặt
- [x] Thêm method OnTriggerEnter2D để xử lý việc nhặt coin
- [x] Thêm UI hiển thị số coin (tùy chọn)

### 3. Thiết lập trong Unity Editor
- [ ] Tạo GameObject Coin Prefab
- [ ] Thêm Collider2D (IsTrigger = true) cho Coin
- [ ] Thêm tag "Coin" cho GameObject
- [ ] Thêm tag "Player" cho Player GameObject
- [ ] Tạo coin material/sprite
- [ ] Đặt coin trong scene để test

### 4. Tối ưu hóa và cải tiến ✅
- [x] Thêm coin spawner (CoinSpawner.cs)
- [x] Thêm hiệu ứng particle khi nhặt coin
- [x] Thêm âm thanh
- [x] Tạo UI hiển thị điểm số (CoinUI.cs)

---

## Chi tiết thực hiện

### Files đã tạo:
1. **Assets/Coin.cs** - Script điều khiển coin
2. **Assets/CoinSpawner.cs** - Script spawn coin tự động
3. **Assets/CoinUI.cs** - Script hiển thị UI coin
4. **Assets/Player.cs** - Đã cập nhật với chức năng nhặt coin

### Bước 1: Thiết lập Tags trong Unity Editor

1. **Tạo Tag "Coin":**
   - Mở Unity Editor
   - Vào `Edit > Project Settings > Tags and Layers`
   - Click `+` trong phần Tags
   - Thêm tag mới: "Coin"

2. **Tạo Tag "Player" (nếu chưa có):**
   - Làm tương tự để tạo tag "Player"

### Bước 2: Tạo Coin Prefab

1. **Tạo GameObject Coin:**
   - `Right click` trong Hierarchy > `Create Empty`
   - Đặt tên: "Coin"
   - Set Position: (0, 0, 0)

2. **Thêm Visual cho Coin:**
   - Add Component > `SpriteRenderer`
   - Chọn sprite cho coin (có thể dùng Unity's default sprites)
   - Set Color thành màu vàng nếu cần

3. **Thêm Collider:**
   - Add Component > `Circle Collider 2D`
   - ✅ **Check "Is Trigger"** (quan trọng!)
   - Adjust radius để phù hợp với sprite

4. **Thêm Script:**
   - Add Component > `Coin` (script đã tạo)
   - Set các giá trị:
     - Coin Value: 1
     - Rotation Speed: 90

5. **Set Tag:**
   - Chọn Coin GameObject
   - Ở Inspector, set Tag = "Coin"

6. **Tạo Prefab:**
   - Drag Coin từ Hierarchy vào Assets folder
   - Xóa Coin trong Hierarchy

### Bước 3: Thiết lập Player

1. **Set Player Tag:**
   - Chọn Player GameObject
   - Set Tag = "Player"

2. **Kiểm tra Player Script:**
   - Đảm bảo Player GameObject có script Player.cs
   - Tất cả các tham số đã được thiết lập đúng

### Bước 4: Tạo UI để hiển thị coin

1. **Tạo Canvas:**
   - `Right click` Hierarchy > `UI > Canvas`

2. **Tạo Text cho hiển thị coin:**
   - `Right click` Canvas > `UI > Text - TextMeshPro`
   - Đặt tên: "CoinCountText"
   - Position ở góc trên bên trái
   - Text: "Coins: 0"

3. **Thêm CoinUI Script:**
   - Tạo Empty GameObject con của Canvas
   - Đặt tên: "CoinUIManager"
   - Add Component > `CoinUI`
   - Drag CoinCountText vào field "Coin Count Text"

### Bước 5: Testing

1. **Đặt Coin vào Scene:**
   - Drag Coin Prefab vào Scene
   - Đặt ở vị trí Player có thể chạm được

2. **Test Game:**
   - Press Play
   - Di chuyển Player đến coin
   - Kiểm tra:
     - Coin có xoay không?
     - Player có nhặt được coin không?
     - UI có hiển thị đúng số coin không?
     - Console có log thông tin không?

### Bước 6: Sử dụng CoinSpawner (Tùy chọn)

1. **Tạo CoinSpawner:**
   - Create Empty GameObject
   - Đặt tên: "CoinSpawner"
   - Add Component > `CoinSpawner`

2. **Thiết lập Spawner:**
   - Coin Prefab: Drag Coin Prefab vào đây
   - Spawn Interval: 3 (giây)
   - Max Coins In Scene: 10
   - Auto Spawn: ✅ Check

3. **Tạo Spawn Points:**
   - Tạo Empty GameObjects làm spawn points
   - Drag vào array Spawn Points

---

## Ghi chú kỹ thuật

### Code Architecture:
- **Coin.cs**: Xử lý logic của từng coin (rotation, collision, destroy)
- **Player.cs**: Xử lý việc thu thập coin và lưu trữ
- **CoinSpawner.cs**: Quản lý việc spawn coin tự động
- **CoinUI.cs**: Hiển thị thông tin coin trên UI

### Key Features:
- ✅ Trigger-based collision detection
- ✅ Automatic coin rotation animation  
- ✅ Sound effects support
- ✅ Particle effects support
- ✅ UI integration with animation
- ✅ Automatic coin spawning system
- ✅ Debug logging

### Collision System:
```csharp
// Coin sử dụng OnTriggerEnter2D
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        // Xử lý thu thập coin
    }
}
```

### Debugging:
- Check Console cho các log messages
- Đảm bảo Tags được set đúng
- Kiểm tra "Is Trigger" được check
- Verify script references

## Công nghệ sử dụng
- Unity 6.2 (6000.2.3f1)
- .NET Framework 4.7.1
- C# Scripts
- 2D Physics System
- UI System (TextMeshPro)
- Audio System

## Troubleshooting

### Coin không biến mất khi Player chạm:
- Check tag "Player" và "Coin"  
- Check "Is Trigger" trên Coin Collider
- Check Player có Collider2D không

### UI không hiển thị số coin:
- Check reference trong CoinUI script
- Check Player script có đúng không

### Coin không xoay:
- Check Rotation Speed > 0 trong Coin script

### Sound không phát:
- Check AudioSource component
- Check AudioClip được assign

---

## Mở rộng thêm

### Có thể thêm:
1. **Multiple Coin Types** - Coin với giá trị khác nhau
2. **Coin Magnet** - Player hút coin từ xa
3. **Coin Trail Effect** - Hiệu ứng đuôi khi coin bay
4. **Coin Shop System** - Dùng coin để mua đồ
5. **Save/Load System** - Lưu số coin khi thoát game
6. **Leaderboard** - Bảng xếp hạng theo coin

### Performance Tips:
- Sử dụng Object Pooling cho coin nếu spawn nhiều
- Giới hạn số coin tối đa trong scene
- Optimize collision detection với Layer-based collision